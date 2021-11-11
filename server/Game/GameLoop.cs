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
        private readonly PowerupManager _powerupManager;

        public GameLoop(IHubContext<GameHub, IGameHub> gameHub, MapManager mapManager, IPlayerManager playerManager, PowerupManager powerupManager)
        {
            _gameHub = gameHub;
            _mapManager = mapManager;
            _playerManager = playerManager;
            _powerupManager = powerupManager;
        }

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
            {
                foreach (var player in _playerManager.GetPlayers())
                {
                    if (_mapManager.IsInExplosion(player.Position) && !player.IsImmortal)
                    {
                        player.IsImmortal = true;
                        Console.WriteLine("Player " + player.ID + " is in explosion");
                        if (player.Lives > 0)
                            await _gameHub.Clients.All.UpdateLives(player.ID, --player.Lives);
                        RemoveImmoratality(player);
                    }
                    
                    if (_mapManager.IsPlayerOnPowerup(player.Position))
                    {
                        await _powerupManager.ApplyPowerup(player.Position, player);
                        await _gameHub.Clients.All.UpdateLives(player.ID, player.Lives);
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