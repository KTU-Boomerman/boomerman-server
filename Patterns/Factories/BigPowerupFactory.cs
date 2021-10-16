using BoomermanServer.Game;
using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;

namespace BoomermanServer.Patterns.Factories
{
    public class BigPowerupFactory : PowerupFactory
    {
        public override HealthPowerup CreateHealthPowerup(Position position)
        {
            return new BigHealthPowerup(position);
        }

        public override SpeedPowerup CreateSpeedPowerup(Position position)
        {
            return new BigSpeedPowerup(position);
        }
    }
}
