using System;
using System.Collections.Generic;
using BoomermanServer.Models.Powerups;
using BoomermanServer.Patterns.Factories;

namespace BoomermanServer.Game
{
    public class PowerupManager
    {
        private readonly MapManager _mapManager;
        private Random _random = new();
        private List<Powerup> _powerups;

        public PowerupManager(MapManager mapManager)
        {
            _mapManager = mapManager;
        }

        public void AddRandomPowerup(Position position)
        {
            var bombChance = _random.NextDouble();
            var factory = GetFactory();
            switch (bombChance)
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

        private PowerupFactory GetFactory()
        {
            var chance = _random.NextDouble();
            if (chance < 0.5)
                return new SmallPowerupFactory();
            else
                return new BigPowerupFactory();
        }

        private void AddPowerup(Powerup powerup)
        {
            _powerups.Add(powerup);
            _mapManager.SetPowerup(powerup);
        }
    }
}