using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Game
{
	public class GameManager : IGameManager
	{
		private GameState _gameState;
		public GameState GameState { get => _gameState; set => _gameState = value; }

		public GameManager()
		{
			_gameState = GameState.PlayersJoining;
		}

		public int GetMinPlayers()
		{
			return 2;
		}

		public void StartGame()
		{
			_gameState = GameState.GameInProgress;
		}
	}
}