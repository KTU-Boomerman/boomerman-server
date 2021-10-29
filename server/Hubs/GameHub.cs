using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models;
using BoomermanServer.Models.Bombs;
using BoomermanServer.Patterns.Adapter;
using BoomermanServer.Patterns.Decorator;
using BoomermanServer.Patterns.Facade;
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
    public class GameHub : Hub
    {
        private readonly ManagerFacade _managerFacade;
        private readonly Queue<Explosion> _pendingExplosions;
        private readonly MapManager _mapManager;
        private Dictionary<BombType, Bomb> _bombs;

        private DiscordApi _discordApi;

        public GameHub(IGameManager gameManager, IPlayerManager playerManager)
        {
            _managerFacade = new ManagerFacade(gameManager, playerManager);
            _pendingExplosions = new Queue<Explosion>();
            _mapManager = new MapManager();
            InitializeBombsDictionary();

            _discordApi = new DiscordApi();
        }

        private void InitializeBombsDictionary()
        {
            var regularDecorator = new BombDecorator(BombType.Regular);

            var waveDecorator = new BombDecorator(BombType.Wave);
            waveDecorator.Component = regularDecorator;

            var pulseDecorator = new BombDecorator(BombType.Pulse);
            pulseDecorator.Component = waveDecorator;

            var boomerangDecorator = new BombDecorator(BombType.Boomerang);
            boomerangDecorator.Component = pulseDecorator;
            
            boomerangDecorator.Execute();
            _bombs = boomerangDecorator.Bombs;
        }

        public async Task PlayerJoin()
        {
            var playerDto = _managerFacade.AddPlayer(Context.ConnectionId).ToDTO();
            await Clients.Others.SendAsync("PlayerJoin", playerDto);

            var playersDto = _managerFacade
                .GetPlayers()
                .Where(p => p.ID != Context.ConnectionId)
                .Select(p => p.ToDTO())
                .ToArray();

            var gameStateDto = new GameStateDTO
            {
                GameState = _managerFacade.GameState.ToString(),
            };

            await Clients.Caller.SendAsync("Joined", playerDto, playersDto, gameStateDto);

            SendNotification("New player!", "Player has joined the game");

            if (_managerFacade.GetPlayerCount() >= _managerFacade.GetMinPlayers())
            {
                _managerFacade.StartGame();
                await ChangeGameState();
            }
        }

        public async Task PlayerLeave()
        {
            _managerFacade.RemovePlayer(Context.ConnectionId);
            await Clients.Others.SendAsync("PlayerLeave", Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await PlayerLeave();
            await base.OnDisconnectedAsync(exception);
        }

        // TODO: calculate and validate positon on backend
        public async Task<PositionValidationDTO> PlayerMove(PositionDTO orgPosition, PositionDTO newPosition)
        {
            if (_managerFacade.GameState != GameState.GameInProgress)
            {
                var originalPostion = _managerFacade.GetPlayer(Context.ConnectionId).Position;
                var positionDto = new PositionDTO { X = originalPostion.X, Y = originalPostion.Y };
                return new PositionValidationDTO { Position = positionDto };
            }

            var returnPos = _mapManager.CheckCollision(new Position(orgPosition), new Position(newPosition));

            _managerFacade.MovePlayer(Context.ConnectionId, returnPos);
            await Clients.Others.SendAsync("PlayerMove", Context.ConnectionId, returnPos.ToDTO());
            return new PositionValidationDTO { Position = returnPos.ToDTO() };
        }

        private async Task ChangeGameState()
        {
            var gameStateDto = new GameStateDTO
            {
                GameState = _managerFacade.GameState.ToString(),
            };

            await Clients.All.SendAsync("GameStateChange", gameStateDto);
        }

        public async Task PlaceBomb(CreateBombDTO bombDTO)
        {
            var player = _managerFacade.GetPlayer(Context.ConnectionId);
            var bomb = _bombs[bombDTO.BombType].Clone();
            bomb.SetPosition(player.Position);
            await Clients.Others.SendAsync("PlayerPlaceBomb", bomb.ToDTO());
            _pendingExplosions.Enqueue(new Explosion(bomb, _pendingExplosions));
        }

        private void SendNotification(string title, string message)
        {
            var gameNotifcation = new GameNotifcation(Clients.All);
            var discordNotification = new DiscordNotification(_discordApi);

            gameNotifcation.Send(title, message);
            discordNotification.Send(title, message);            
        }
    }
}
