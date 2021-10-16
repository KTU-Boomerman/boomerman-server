using BoomermanServer.Game;
using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;

namespace BoomermanServer.Patterns.Factories
{
    public class SmallPowerupFactory : PowerupFactory
    {
        public override HealthPowerup CreateHealthPowerup(Position position)
        {
            return new SmallHealthPowerup(position);
        }

        public override SpeedPowerup CreateSpeedPowerup(Position position)
        {
            return new SmallSpeedPowerup(position);
        }
    }
}
