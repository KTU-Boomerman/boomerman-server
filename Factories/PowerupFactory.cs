using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;
using BoomermanServer.Game;

namespace BoomermanServer.Factories
{
    public abstract class PowerupFactory
    {
        public abstract HealthPowerup CreateHealthPowerup(Position position);

        public abstract SpeedPowerup CreateSpeedPowerup(Position position);
    }
}
