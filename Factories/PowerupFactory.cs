using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;

namespace BoomermanServer.Factories
{
    public abstract class PowerupFactory
    {
        public abstract HealthPowerup CreateHealthPowerup();

        public abstract SpeedPowerup CreateSpeedPowerup();
    }
}
