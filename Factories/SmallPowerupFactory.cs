using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;
using BoomermanServer.Game;

namespace BoomermanServer.Factories
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
