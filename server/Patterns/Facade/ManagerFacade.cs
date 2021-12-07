using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Models.Explosions;
using BoomermanServer.Models.Powerups;

namespace BoomermanServer.Patterns.Facade
{
    public class ManagerFacade
    {
        private readonly IGameManager _gameManager;
        private readonly IPlayerManager _playerManager;
        private readonly MapManager _mapManager;

        public ManagerFacade(IGameManager gameManager, IPlayerManager playerManager, MapManager mapManager)
        {
            _gameManager = gameManager;
            _playerManager = playerManager;
            _mapManager = mapManager;
        }

        public Player AddPlayer(string id) => _playerManager.AddPlayer(id);

        public void RemovePlayer(string id) => _playerManager.RemovePlayer(id);

        public void MovePlayer(string id, Position position) => _playerManager.MovePlayer(id, position);

        public Player GetPlayer(string id) => _playerManager.GetPlayer(id);

        public int GetPlayerCount() => _playerManager.GetPlayerCount();

        public List<Player> GetPlayers() => _playerManager.GetPlayers();

        public  void RestorePlayer(string id) => _playerManager.RestorePlayer(id);

        public int GetMinPlayers() => _gameManager.GetMinPlayers();

        public void StartGame() => _gameManager.StartGame();

        public GameState GameState => _gameManager.GameState;

        public Position CheckCollision(Position originalPos, Position newPos) => _mapManager.CheckCollision(originalPos, newPos);

        public List<Position> GetDestructibleWalls() => _mapManager.GetDestructibleWalls();

        public Position SnapBombPosition(Position position) => _mapManager.SnapBombPosition(position);

        public Explosions FilterExplosions(Explosions explosions) => _mapManager.FilterExplosions(explosions);

        public void SetExplosion(Position position) => _mapManager.SetExplosion(position);

        public void SetGrass(Position position) => _mapManager.SetGrass(position);

        public bool IsInExplosion(Position position) => _mapManager.IsInExplosion(position);

        public Position GetExplosionPosition(Position position) => _mapManager.GetExplosionPosition(position);

        public bool IsDestructible(Position position) => _mapManager.IsDestructible(position);

        public void SetPowerup(Powerup powerup) => _mapManager.SetPowerup(powerup);

        public bool IsPlayerOnPowerup(Position position) => _mapManager.IsPlayerOnPowerup(position);

        public Position SnapPosition(Position position) => _mapManager.SnapPosition(position);
    }
}
