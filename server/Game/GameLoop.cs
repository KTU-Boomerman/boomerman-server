using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoomermanServer.Hubs;
using BoomermanServer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace BoomermanServer.Game
{
    public class GameLoop : BackgroundService
    {
        private readonly TimeSpan Interval = TimeSpan.FromMilliseconds(10);
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly MapManager _mapManager;
        private readonly IPlayerManager _playerManager;
        private readonly PowerupManager _powerupManager;
        private readonly BombManager _bombManager;
        private IExplosionQueue _explosionQueue;

        private List<Explosion> _activeExplosions;

        private TimeSpan _mementoTimer;

        public GameLoop(IHubContext<GameHub, IGameHub> gameHub, MapManager mapManager, IPlayerManager playerManager, PowerupManager powerupManager, IExplosionQueue explosionQueue, BombManager bombManager)
        {
            _gameHub = gameHub;
            _mapManager = mapManager;
            _playerManager = playerManager;
            _powerupManager = powerupManager;
            _explosionQueue = explosionQueue;
            _bombManager = bombManager;

            _activeExplosions = new List<Explosion>();
            _mementoTimer = TimeSpan.FromSeconds(5) + DateTime.Now.TimeOfDay;
        }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
            {
                var explosions = _explosionQueue.Where(e => e.ShouldExplode()).ToList();

                foreach (var explosion in explosions)
                {
                    _explosionQueue.Remove(explosion);
                    _activeExplosions.Add(explosion);
                }

                _activeExplosions.RemoveAll(e => e.Exploded);

                if (_mementoTimer < DateTime.Now.TimeOfDay)
                {
                    _playerManager.SaveMemento();
                    _mementoTimer = TimeSpan.FromSeconds(5) + DateTime.Now.TimeOfDay;
                }

                await ProcessPlayers(_activeExplosions);
                await ProcessExplosions(explosions);
                await ProcessPowerups();

                await Task.Delay(Interval);
            }
		}

        private async Task ProcessExplosions(List<Explosion> explosions)
        {
            foreach (var explosion in explosions)
            {
                var spawnItem = _mapManager.IsDestructible(explosion.Position);

                _bombManager.RemoveBomb(explosion.Owner, explosion.Position);
                await _gameHub.Clients.All.UpdateBombCount(explosion.Owner.ID, _bombManager.GetPlayerBombCount(explosion.Owner));

                if (_mapManager.IsDestructible(explosion.Position))
                {
                    var player = explosion.Owner;
                    player.Score += ActionScore.DestroyedWall;
                    await _gameHub.Clients.All.UpdateScore(player.ID, player.Score);
                }
                
                _mapManager.SetExplosion(explosion.Position);
                Console.WriteLine($"Explosion at {explosion.Position.ToString()} on {DateTime.Now}");
                RemoveExplosion(explosion.Position, spawnItem, explosion);
            }
            
            var explosionPositions = explosions.Select(e => e.Position.ToDTO()).ToArray();
            await _gameHub.Clients.All.Explosions(explosionPositions);
        }

        private async Task ProcessPlayers(List<Explosion> explosions)
        {
            foreach (var player in _playerManager.GetPlayers())
            {
                var explosionPosition = _mapManager.GetExplosionPosition(player.Position);
                var explosion = explosions.FirstOrDefault(e => e.Position == explosionPosition);

                if (explosion == null || player.IsImmortal || player.Lives == 0) continue;

                var killer = explosion.Owner;

                player.IsImmortal = true;
                Console.WriteLine("Player " + player.ID + " is in explosion");

                if (player.Lives > 0)
                    await _gameHub.Clients.All.UpdateLives(player.ID, --player.Lives);

                if (player.Lives == 0 && player != killer)
                {
                    killer.Score += ActionScore.KilledPlayer;
                    await _gameHub.Clients.All.UpdateScore(killer.ID, killer.Score);
                }

                RemoveImmoratality(player);
            }
        }

        private async Task ProcessPowerups()
        {
            foreach (var player in _playerManager.GetPlayers())
            {
                if (_mapManager.IsPlayerOnPowerup(player.Position) && player.Lives > 0)
                {
                    await _powerupManager.ApplyPowerup(player.Position, player);
                    await _gameHub.Clients.All.UpdateLives(player.ID, player.Lives);
                }
            }
        }

        private void RemoveImmoratality(Player player)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                player.IsImmortal = false;
            });
        }
        
        private void RemoveExplosion(Position position, bool spawnItem, Explosion explosion)
        {
            Task.Run(async () =>
            {
                await Task.Delay(1000);
                explosion.Exploded = true;
                if (spawnItem)
                    _powerupManager.AddRandomPowerup(position);
                else
                    _mapManager.SetGrass(position);
            });
        }
	}
}