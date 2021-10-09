using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Game
{
	public interface IGameManager
	{
		GameState GameState { get; set; }
		int GetMinPlayers();

		void StartGame();
	}
}