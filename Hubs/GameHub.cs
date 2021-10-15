using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models;
using System;
using System.Linq;
using System.Collections.Generic;

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
        private readonly IGameManager _gameManager;
        private readonly IPlayerManager _playerManager;
        private readonly Queue<Explosion> _pendingExplosions;

        public GameHub(IGameManager gameManager, IPlayerManager playerManager)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
            _pendingExplosions = new Queue<Explosion>();
        }

        public async Task PlayerJoin()
        {
            var playerDto = _playerManager.AddPlayer(Context.ConnectionId).ToDTO();
            await Clients.Others.SendAsync("PlayerJoin", playerDto);

            var playersDto = _playerManager
                .GetPlayers()
                .Where(p => p.ID != Context.ConnectionId)
                .Select(p => p.ToDTO())
                .ToArray();

            var gameStateDto = new GameStateDTO
            {
                GameState = _gameManager.GameState.ToString(),
            };

            await Clients.Caller.SendAsync("Joined", playerDto, playersDto, gameStateDto);

            if (_playerManager.GetPlayerCount() >= _gameManager.GetMinPlayers())
            {
                _gameManager.StartGame();
                await ChangeGameState();
            }
        }

        public async Task PlayerLeave()
        {
            _playerManager.RemovePlayer(Context.ConnectionId);
            await Clients.Others.SendAsync("PlayerLeave", Context.ConnectionId);
        }
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await PlayerLeave();
            await base.OnDisconnectedAsync(exception);
        }

        // TODO: calculate and validate positon on backend
        public async Task<PositionValidationDTO> PlayerMove(PositionDTO position)
        {
            if (_gameManager.GameState != GameState.GameInProgress)
            {
                var originalPostion = _playerManager.GetPlayer(Context.ConnectionId).Position;
                var positionDto = new PositionDTO { X = originalPostion.X, Y = originalPostion.Y };
                return new PositionValidationDTO { IsValid = false, Position = positionDto };
            }

            _playerManager.MovePlayer(Context.ConnectionId, new Position(position));
            await Clients.Others.SendAsync("PlayerMove", Context.ConnectionId, position);
            return new PositionValidationDTO { IsValid = true };
        }

        private async Task ChangeGameState()
        {
            var gameStateDto = new GameStateDTO
            {
                GameState = _gameManager.GameState.ToString(),
            };
            await Clients.All.SendAsync("GameStateChange", gameStateDto);
        }

        public async Task PlaceBomb(PositionDTO positionDTO)
        {
            var position = new Position(positionDTO);
            var bomb = new RegularBomb(position); // TODO: Add logic to see what type of bomb to create
            await Clients.All.SendAsync("PlayerPlaceBomb", bomb.ToDTO());
            _pendingExplosions.Enqueue(new Explosion(bomb));
        }
    }
}