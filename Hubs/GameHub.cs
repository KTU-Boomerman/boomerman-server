using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;
using System;
using System.Linq;
using System.Diagnostics;

namespace BoomermanServer.Hubs
{
	/*
    * GameHub is the hub that handles all the game logic.
    * It is responsible for creating the game, updating the game, and sending the game to the clients.
    * 
    * Events:
    * -GameStart - game started, players can move
    * -PlayerJoin - player joined, hes assigned an id
    * -PlayerLeave
    * -PlayerMove
    * PlayerPlaceBomb
    * BombExplode - explosion in tiles and removed walls
    * PowerupPickup
    * GameEnd - game ended, players can't move, winner is shown
	* GameStateChange 
	*/
	public class GameHub : Hub
	{
		private readonly IGameManager _gameManager;
		private readonly IPlayerManager _playerManager;

		public GameHub(IGameManager gameManager, IPlayerManager playerManager)
		{
			_gameManager = gameManager;
			_playerManager = playerManager;
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

			await Clients.Caller.SendAsync("Joined", playerDto, playersDto);

			if (_playerManager.GetPlayerCount() >= _gameManager.GetMinPlayers())
			{
				_gameManager.StartGame();
				// TODO: refactor to on game state change - one event for all game states
				await Clients.All.SendAsync("GameStart");
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
			if (_gameManager.GameState != GameState.GameInProgress) {
				var originalPostion = _playerManager.GetPlayer(Context.ConnectionId).Position;
				var positionDto = new PositionDTO { X = originalPostion.X, Y = originalPostion.Y };
				return new PositionValidationDTO { IsValid = false, Position = positionDto };
			}

			_playerManager.MovePlayer(Context.ConnectionId, new Position(position));
			await Clients.Others.SendAsync("PlayerMove", Context.ConnectionId, position);
			return new PositionValidationDTO { IsValid = true };
		}
	}
}