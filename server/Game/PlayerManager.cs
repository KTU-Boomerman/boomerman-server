using System;
using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Patterns.Memento;

namespace BoomermanServer.Game
{
    public class PlayerManager : IPlayerManager
    {
        // TODO: refactor once we have maps with their spawn points
        private readonly Position[] _spawnPoints = new Position[]{
            new Position(32, 32),
            new Position(480, 32),
            new Position(32, 352),
            new Position(480, 352),
        };
        private Dictionary<string, Player> _players;
        private Dictionary<string, UnwindCaretaker> _caretakers;
        
        public PlayerManager()
        {
            _players = new Dictionary<string, Player>();
            _caretakers = new Dictionary<string, UnwindCaretaker>();
        }

        public Player AddPlayer(string id)
        {
            lock(_players) {
                if (_players.ContainsKey(id)) {
                    return _players[id];
                }

                Position spawnPoint = GetRandomSpawnPoint();
                Player player = new Player(id, spawnPoint);

                _players.Add(id, player);
                _caretakers.Add(id, new UnwindCaretaker());
            
                return player;
            }
        }

        public Player GetPlayer(string id)
        {
            return _players[id];
        }

        public void RemovePlayer(string id)
        {
            lock(_players) {
                _players.Remove(id);
                _caretakers.Remove(id);
            }
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

        private Position GetRandomSpawnPoint()
        {
            int spawnPointIndex = new Random().Next(_spawnPoints.Length);
            Position spawnPoint = _spawnPoints[spawnPointIndex];
            return spawnPoint;
        }

        public void RestorePlayer(string id)
        {
            UnwindCaretaker caretaker = _caretakers[id];
            caretaker.Undo();
        }

        public void SavePlayer(string id)
        {
            Player player = _players[id];
            UnwindCaretaker caretaker = _caretakers[id];
            caretaker.Save(player.GetState());
        }

		public void SaveMemento()
		{
			foreach (var player in _players)
            {
                SavePlayer(player.Key);
            }
		}
	}
}
