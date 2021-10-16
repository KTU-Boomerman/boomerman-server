using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;
using BoomermanServer.Game;

namespace BoomermanServer.Factories
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
