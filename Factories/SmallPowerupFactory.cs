using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;

namespace BoomermanServer.Factories
{
    public class SmallPowerupFactory : PowerupFactory
    {
        public override HealthPowerup CreateHealthPowerup()
        {
            return new SmallHealthPowerup();
        }

        public override SpeedPowerup CreateSpeedPowerup()
        {
            return new SmallSpeedPowerup();
        }
    }
}
