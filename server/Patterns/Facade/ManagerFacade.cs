using System.Collections.Generic;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Facade
{
    public class ManagerFacade
    {
        private readonly IGameManager _gameManager;
        private readonly IPlayerManager _playerManager;

        public ManagerFacade(IGameManager gameManager, IPlayerManager playerManager)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
        }

        public void AddPlayer(string id) => _playerManager.AddPlayer(id);

        public void RemovePlayer(string id) => _playerManager.RemovePlayer(id);

        public void MovePlayer(string id, Position position) => _playerManager.MovePlayer(id, position);

        public Player GetPlayer(string id) => _playerManager.GetPlayer(id);

        public int GetPlayerCount() => _playerManager.GetPlayerCount();

        public List<Player> GetPlayers => _playerManager.GetPlayers();

        public int GetMinPlayers() => _gameManager.GetMinPlayers();

        public void StartGame() => _gameManager.StartGame();
    }
}
