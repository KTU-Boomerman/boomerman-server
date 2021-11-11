using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BoomermanServer.Hubs;
using BoomermanServer.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace BoomermanServer.Game
{
    public class ExplosionBroadcaster : BackgroundService
    {
        private readonly TimeSpan _interval = TimeSpan.FromMilliseconds(10);
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly MapManager _mapManager;
        private readonly PowerupManager _powerupManager;
        private IExplosionQueue _explosionQueue;

        public ExplosionBroadcaster(IHubContext<GameHub, IGameHub> gameHub, IExplosionQueue explosionQueue, MapManager mapManager,
                                    PowerupManager powerupManager)
        {
            _gameHub = gameHub;
            _mapManager = mapManager;
            _explosionQueue = explosionQueue;
            _powerupManager = powerupManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var explosions = _explosionQueue.Where(e => e.ShouldExplode()).ToList();

                foreach (var explosion in explosions)
                {
                    _explosionQueue.Remove(explosion);
                    var spawnItem = _mapManager.IsDestructible(explosion.Position);

                    if (_mapManager.IsDestructible(explosion.Position))
                    {
                        var player = explosion.Owner;
                        player.Score += ActionScore.DestroyedWall;
                        await _gameHub.Clients.All.UpdateScore(player.ID, player.Score);
                    }
                    
                    _mapManager.SetExplosion(explosion.Position);
                    Console.WriteLine($"Explosion at {explosion.Position.ToString()} on {DateTime.Now}");
                    RemoveExplosion(explosion.Position, spawnItem);
                }
                
                var explosionPositions = explosions.Select(e => e.Position.ToDTO()).ToArray();
                await _gameHub.Clients.All.Explosions(explosionPositions);

                await Task.Delay(_interval);
            }
        }

        private async void RemoveExplosion(Position position, bool spawnItem)
        {
            await Task.Delay(1000);
            if (spawnItem)
            {
                _powerupManager.AddRandomPowerup(position);
            }
            else
                _mapManager.SetGrass(position);
        }
	}
}