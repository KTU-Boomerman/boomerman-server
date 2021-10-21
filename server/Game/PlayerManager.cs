using System;
using System.Collections.Generic;
using System.Linq;

namespace BoomermanServer.Game
{
    public class PlayerManager : IPlayerManager
    {
        // TODO: refactor once we have maps with their spawn points
        private readonly Position[] _spawnPoints = new Position[]{
            new Position(100, 100),
        };
        public Dictionary<string, Player> Players { get; set; }
        public PlayerManager()
        {
            Players = new Dictionary<string, Player>();
        }

        public Player AddPlayer(string id)
        {
            lock(Players) {
                Position spawnPoint = getRandomSpawnPoint();
                Player player = new Player(id, spawnPoint);

                Players.Add(id, player);
            
                return player;
            }
        }

        public Player GetPlayer(string id)
        {
            return Players[id];
        }

        public void RemovePlayer(string id)
        {
            lock(Players) {
                Players.Remove(id);
            }
        }

        public void MovePlayer(string id, Position position)
        {
            Player player = Players[id];
            player.Position = position;
        }

        public int GetPlayerCount()
        {
            return Players.Count;
        }

        public List<Player> GetPlayers()
        {
            return Players.Values.ToList();
        }

        private Position getRandomSpawnPoint()
        {
            int spawnPointIndex = new Random().Next(_spawnPoints.Length);
            Position spawnPoint = _spawnPoints[spawnPointIndex];
            return spawnPoint;
        }
    }
}
