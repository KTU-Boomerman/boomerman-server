using System;
using System.Collections.Generic;
using BoomermanServer.Hubs;
using BoomermanServer.Models.Powerups;
using BoomermanServer.Patterns.Factories;
using Microsoft.AspNetCore.SignalR;

namespace BoomermanServer.Game
{
    public class PowerupManager
    {
        private readonly IHubContext<GameHub, IGameHub> _gameHub;
        private readonly MapManager _mapManager;
        private Random _random = new();
        private List<Powerup> _powerups;

        public PowerupManager(IHubContext<GameHub, IGameHub> gameHub, MapManager mapManager)
        {
            _gameHub = gameHub;
            _mapManager = mapManager;
            _powerups = new List<Powerup>();
        }

        public void AddRandomPowerup(Position position)
        {
            if (_random.NextDouble() < 0.2)
            {
                var factory = GetFactory();
                switch (_random.NextDouble())
                {
                    case < 0.33:
                        AddPowerup(factory.CreateHealthPowerup(position));
                        break;
                    case >= 0.33 and < 0.67:
                        AddPowerup(factory.CreateSpeedPowerup(position));
                        break;
                    case >= 0.67:
                        AddPowerup(factory.CreateBombCountPowerup(position));
                        break;
                }
            }
            else
            {
                _mapManager.SetGrass(position);
            }
        }

        private PowerupFactory GetFactory()
        {
            var chance = _random.NextDouble();
            if (chance < 0.5)
                return new SmallPowerupFactory();
            else
                return new BigPowerupFactory();
        }

        private async void AddPowerup(Powerup powerup)
        {
            _powerups.Add(powerup);
            _mapManager.SetPowerup(powerup);
            await _gameHub.Clients.All.PlacePowerup(powerup.ToDTO());
        }
    }
}