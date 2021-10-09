using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Game
{
	public interface IPlayerManager
	{
		Dictionary<string, Player> Players { get; set; }
		Player AddPlayer(string id);
		Player GetPlayer(string id);
		void RemovePlayer(string id);
		void MovePlayer(string id, Position position);
		int GetPlayerCount();
		List<Player> GetPlayers();
	}
}