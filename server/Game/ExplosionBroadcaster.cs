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
    public class ExplosionBroadcaster : BackgroundService
    {
        private readonly TimeSpan Interval = TimeSpan.FromMilliseconds(40);
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly MapManager _mapManager;
        private IExplosionQueue _explosionQueue;

        public ExplosionBroadcaster(IHubContext<GameHub, IGameHub> gameHub, IExplosionQueue explosionQueue, MapManager mapManager)
        {
            _gameHub = gameHub;
            _mapManager = mapManager;
            _explosionQueue = explosionQueue;
        }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
            {
                var explosion = _explosionQueue.FirstOrDefault();

                if (explosion != null && explosion.ShouldExplode())
                {
                    _explosionQueue.Remove(explosion);
                    _mapManager.SetExplosion(explosion.Position);
                    Console.WriteLine($"Explosion at {explosion.Position.ToString()} on {DateTime.Now}");
                    await _gameHub.Clients.All.Explosion(explosion.Position.ToDTO());
                    RemoveGrass(explosion.Position);
                }
                await Task.Delay(Interval);
            }
		}

        private async void RemoveGrass(Position position)
        {
            await Task.Delay(1000);
            _mapManager.SetGrass(position);
        }
	}
}