using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Game
{
	public class PlayerManager : IPlayerManager
	{
		// TODO: refactor once we have maps with their spawn points
		private readonly Position[] _spawnPoints = new Position[]{
			new Position(100, 100),
		};

		private Dictionary<string, Player> _players;
		public Dictionary<string, Player> Players { get => _players; set => _players = value; }
		public PlayerManager()
		{
			_players = new Dictionary<string, Player>();
		}

		public Player AddPlayer(string id)
		{
			Position spawnPoint = getRandomSpawnPoint();
			Player player = new Player(id, spawnPoint);

			_players.Add(id, player);

			return player;
		}

		public Player GetPlayer(string id)
		{
			return _players[id];
		}

		public void RemovePlayer(string id)
		{
			_players.Remove(id);
		}

		public void MovePlayer(string id, Position position)
		{
			Player player = _players[id];
			player.Position = position;
		}

		public int GetPlayerCount()
		{
			return _players.Count;
		}

		public List<Player> GetPlayers()
		{
			return _players.Values.ToList();
		}

		private Position getRandomSpawnPoint()
		{
			int spawnPointIndex = new Random().Next(_spawnPoints.Length);
			Position spawnPoint = _spawnPoints[spawnPointIndex];
			return spawnPoint;
		}
	}
}