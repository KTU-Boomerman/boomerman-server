using System;
using System.Threading;
using System.Threading.Tasks;
using BoomermanServer.Hubs;
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

        public GameLoop(IHubContext<GameHub, IGameHub> gameHub, MapManager mapManager, IPlayerManager playerManager)
        {
            _gameHub = gameHub;
            _mapManager = mapManager;
            _playerManager = playerManager;
        }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var player in _playerManager.GetPlayers())
                {
                    // Console.WriteLine($"{player.ID} is moving");
                    if (_mapManager.IsInExplosion(player.Position) && !player.IsImmortal)
                    {
                        player.IsImmortal = true;
                        Console.WriteLine("Player " + player.ID + " is in explosion");
                        await _gameHub.Clients.All.UpdateLives(--player.Lives);

                        RemoveImmoratality(player);
                    }
                }
                
                await Task.Delay(Interval);
            }
		}

        private void RemoveImmoratality(Player player)
        {
            Task.Run(async () =>
            {
                await Task.Delay(TimeSpan.FromSeconds(1));
                player.IsImmortal = false;
            });
        }
	}
}