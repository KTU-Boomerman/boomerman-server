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
using BoomermanServer.Patterns.Strategy;
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
        private readonly MapManager _mapManager;
        private Dictionary<BombType, Bomb> _bombs;

        private IExplosionQueue _explosionQueue;
        private ExplosionContext _explosionContext;

        private DiscordApi _discordApi;

        public GameHub(IGameManager gameManager, IPlayerManager playerManager, IExplosionQueue explosionQueue)
        {
            _managerFacade = new ManagerFacade(gameManager, playerManager);
            _pendingExplosions = new Queue<Explosion>();
            _mapManager = new MapManager();
            _explosionQueue = explosionQueue;
            InitializeBombsDictionary();
            _explosionContext = new ExplosionContext(new BasicExplosion());

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
            await Clients.Others.PlayerJoin(playerDto);

            var playersDto = _managerFacade
                .GetPlayers()
                .Where(p => p.ID != Context.ConnectionId)
                .Select(p => p.ToDTO())
                .ToArray();

            var gameStateDto = new GameStateDTO
            {
                GameState = _managerFacade.GameState.ToString(),
            };

            await Clients.Caller.Joined(playerDto, playersDto, gameStateDto, _mapManager.mapJSON());

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

            var currentPosition = _mapManager.CheckCollision(new Position(originalPosition), new Position(newPosition));

            _managerFacade.MovePlayer(Context.ConnectionId, currentPosition);
            await Clients.Others.PlayerMove(Context.ConnectionId, currentPosition.ToDTO());

            return currentPosition.ToDTO();
        }

        private async Task ChangeGameState()
        {
            var gameStateDto = new GameStateDTO
            {
                GameState = _managerFacade.GameState.ToString(),
            };

            await Clients.All.GameStateChange(gameStateDto);
        }

        public async Task<PositionDTO> PlaceBomb(CreateBombDTO bombDTO)
        {
            var player = _managerFacade.GetPlayer(Context.ConnectionId);
            var bomb = _bombs[bombDTO.BombType].Clone();
            var bombPosition = _mapManager.SnapBombPosition(player.Position);
            bomb.SetPosition(bombPosition);
            await Clients.Others.PlayerPlaceBomb(bomb.ToDTO());

            // await Task.Delay(bomb.GetBombType().GetPlacementTime());
            // await bomb.Remove();
            var explosions = _explosionContext.GetExplosions(bombPosition, TimeSpan.FromSeconds(2));
            _explosionQueue.UnionWith(explosions);

            return bombPosition.ToDTO();
            // _explosionQueue.Add();
            // _pendingExplosions.Enqueue(new Explosion(bomb, _pendingExplosions));
        }

        private void SendNotification(string title, string message)
        {
            // var gameNotifcation = new GameNotifcation(this);
            // var discordNotification = new DiscordNotification(_discordApi);

            // gameNotifcation.Send(title, message);
            // discordNotification.Send(title, message);            
        }

		public async Task Explosion(Position position)
		{
			await Clients.All.Explosion(position.ToDTO());
		}
	}
}
