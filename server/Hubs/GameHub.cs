using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models;
using BoomermanServer.Models.Bombs;
using BoomermanServer.Patterns.Adapter;
using BoomermanServer.Patterns.ChainOfResponsibility;
using BoomermanServer.Patterns.Decorator;
using BoomermanServer.Patterns.Facade;
using BoomermanServer.Patterns.Iterator;
using BoomermanServer.Patterns.Mediator;
using BoomermanServer.Patterns.Strategy;
using BoomermanServer.Patterns.Template;
using Microsoft.AspNetCore.SignalR;

namespace BoomermanServer.Hubs
{
    /*
    * GameHub is the hub that handles all the game logic.
    * It is responsible for creating the game, updating the game, and sending the game to the clients.
    * 
    * Events:
	* GameStateChange 
    * -PlayerJoin - player joined, hes assigned an id
    * -PlayerLeave
    * -PlayerMove
    * PlayerPlaceBomb
    * BombExplode - explosion in tiles and removed walls
    * PowerupPickup
	*/
    public class GameHub : Hub<IGameHub>
    {

        private readonly ManagerFacade _managerFacade;
        private readonly Queue<Explosion> _pendingExplosions;
        //private readonly MapManager _mapManager;
        private readonly BombManager _bombManager;
        private Dictionary<BombType, Bomb> _bombsPrototypes;

        private IExplosionQueue _explosionQueue;
        private ExplosionContext _explosionContext;

        private DiscordApi _discordApi;
        private ChatHandler _chatHandler;

        private static Dictionary<string, IIterator> _playerColorIterators = new Dictionary<string, IIterator>();
        private static IChatroom _chatroom = new Chatroom();

        private IIterator _playerColorIterator;

        public GameHub(IGameManager gameManager, IPlayerManager playerManager, IExplosionQueue explosionQueue, MapManager mapManager, BombManager bombManager)
        {
            _managerFacade = new ManagerFacade(gameManager, playerManager, mapManager);
            _pendingExplosions = new Queue<Explosion>();
            _bombManager = bombManager;
            _explosionQueue = explosionQueue;
            InitializeBombsDictionary();
            _explosionContext = new ExplosionContext(new BasicExplosion());

            _discordApi = new DiscordApi();
            _chatHandler = new ChatHandler(playerManager);

            _playerColorIterator = new ColorPalette().GetIterator();
        }

        private void InitializeBombsDictionary()
        {
            var regularCreator = new BombCreator(BombType.Regular);

            var waveDecorator = new BombCreator(BombType.Wave);
            waveDecorator.Component = regularCreator;

            var pulseCreator = new BombCreator(BombType.Pulse);
            pulseCreator.Component = waveDecorator;

            var boomerangCreator = new BombCreator(BombType.Boomerang);
            boomerangCreator.Component = pulseCreator;

            boomerangCreator.Execute();
            _bombsPrototypes = boomerangCreator.Bombs;
        }

        public async Task PlayerJoin()
        {
            var playerDto = _managerFacade.AddPlayer(Context.ConnectionId).ToDTO();
            await Clients.Others.PlayerJoin(playerDto);

            _chatroom.Register(_managerFacade.GetPlayer(playerDto.ID));

            var playersDto = _managerFacade
                .GetPlayers()
                .Where(p => p.ID != Context.ConnectionId)
                .Select(p => p.ToDTO())
                .ToArray();

            var gameStateDto = new GameStateDTO
            {
                GameState = _managerFacade.GameState.ToString(),
            };

            var mapExtractor = new MapExtractor(_managerFacade);
            var mapDTO = mapExtractor.Extract() as MapDTO;

            await Clients.Caller.Joined(playerDto, playersDto, gameStateDto, mapDTO);
            await Clients.All.UpdateBombCount(playerDto.ID, 1);


            SendNotification("New player!", "Player has joined the game");

            if (_managerFacade.GetPlayerCount() >= _managerFacade.GetMinPlayers())
            {
                var players = new PlayerContainer(_managerFacade.GetPlayers());
                // var command = new ImmortalitySetter(players);
                // command.SetAttributes();

                _managerFacade.StartGame();

                // command.Undo();
                await ChangeGameState();
            }
        }

        private object PlayerContainer()
        {
            throw new NotImplementedException();
        }

        public async Task PlayerLeave()
        {
            _managerFacade.RemovePlayer(Context.ConnectionId);
            await Clients.Others.PlayerLeave(Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await PlayerLeave();
            await base.OnDisconnectedAsync(exception);
        }

        public async Task<PositionDTO> PlayerMove(PositionDTO originalPosition, PositionDTO newPosition)
        {
            if (_managerFacade.GameState != GameState.GameInProgress)
            {
                return _managerFacade.GetPlayer(Context.ConnectionId).Position.ToDTO();
            }

            var currentPosition = _managerFacade.CheckCollision(new Position(originalPosition), new Position(newPosition));

            _managerFacade.MovePlayer(Context.ConnectionId, currentPosition);
            await Clients.Others.PlayerMove(Context.ConnectionId, currentPosition.ToDTO());

            return currentPosition.ToDTO();
        }

        private async Task ChangeGameState()
        {
            var dtoExtractor = new GameStateExtractor(_managerFacade);
            var gameStateDto = dtoExtractor.Extract() as GameStateDTO;

            await Clients.All.GameStateChange(gameStateDto);
        }

        public async Task PlaceBomb(CreateBombDTO bombDTO)
        {
            // Place bomb
            var player = _managerFacade.GetPlayer(Context.ConnectionId);

            if (player.Lives <= 0)
                return;

            if (_bombManager.GetPlayerActiveBombCount(player) < player.MaxBombCount)
            {
                var bombType = bombDTO.BombType;
                var bomb = _bombsPrototypes[bombType].Clone();
                if (bomb.BombWeight <= player.MaxBombCount)
                {
                    bomb.Owner = player;
                    var bombPosition = _managerFacade.SnapBombPosition(player.Position);
                    bomb.SetPosition(bombPosition);
                    _bombManager.AddBomb(bomb);
                    await Clients.All.PlayerPlaceBomb(bomb.ToDTO());
                    await Clients.All.UpdateBombCount(player.ID, _bombManager.GetPlayerBombCount(player));

                    // Change explosion strategy
                    IExplosionStrategy strategy = bombType switch
                    {
                        BombType.Wave => new WaveExplosion(),
                        BombType.Pulse => new PulseExplosion(),
                        BombType.Boomerang => new BoomerangExplosion(),
                        _ => new BasicExplosion(),
                    };
                    _explosionContext.SetStrategy(strategy);

                    // Enqueue explosions
                    var explosions = _explosionContext.GetExplosions(bombPosition, TimeSpan.FromSeconds(2), player);
                    var filteredExplosions = _managerFacade.FilterExplosions(explosions);
                    _explosionQueue.UnionWith(filteredExplosions.ToList());
                }
            }
        }

        public async Task ChangePlayerColor()
        {
            var player = _managerFacade.GetPlayer(Context.ConnectionId);
            var colorIterator = GetPlayerColorIterator(player.ID);
            var colorDto = new PlayerColorDTO
            {
                Color = (PlayerColor)colorIterator.Next()
            };
            await Clients.All.PlayerChangeColor(player.ID, colorDto);
        }

        private IIterator GetPlayerColorIterator(string playerId)
        {
            if (!_playerColorIterators.ContainsKey(playerId))
            {
                var player = _managerFacade.GetPlayer(playerId);
                _playerColorIterators.Add(playerId, player.ColorPalette.GetIterator());
            }
            return _playerColorIterators[playerId];
        }

        private void SendNotification(string title, string message)
        {
            var gameNotification = new GameNotifcation(this);
            var discordNotification = new DiscordNotification(_discordApi);

            gameNotification.Send(title, message);
            discordNotification.Send(title, message);
        }

        public async Task SendMessage(string text)
        {
            var player = _managerFacade.GetPlayer(Context.ConnectionId);

            var message = new Message
            {
                PlayerID = player.ID,
                PlayerName = player.Name,
                Text = text,
            };
            var handledMessage = _chatHandler.Handle(message);
            await Clients.All.SendMessage(player.ID, handledMessage.PlayerName, handledMessage.Text);
        }

        public async Task PlayerUnwind()
        {
            var player = _managerFacade.GetPlayer(Context.ConnectionId);
            
            Console.WriteLine($"Player {player.Name} has unwinded");

            if (player.Lives > 0)
            {
                _managerFacade.RestorePlayer(player.ID);

                var updatedPlayer = _managerFacade.GetPlayer(player.ID);
                await Clients.All.UnwindPlayer(updatedPlayer.ID, updatedPlayer.ToDTO());
            }
        }
    }
}
