using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;

namespace BoomermanServer.Factories
{
    public class BigPowerupFactory : PowerupFactory
    {
        public override HealthPowerup CreateHealthPowerup()
        {
            return new BigHealthPowerup();
        }

        public override SpeedPowerup CreateSpeedPowerup()
        {
            return new BigSpeedPowerup();
        }
    }
}
