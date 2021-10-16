using BoomermanServer.Game;
using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;

namespace BoomermanServer.Patterns.Factories
{
    public abstract class PowerupFactory
    {
        public abstract HealthPowerup CreateHealthPowerup(Position position);

        public abstract SpeedPowerup CreateSpeedPowerup(Position position);
    }
}
