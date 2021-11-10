using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Health
{
    public class SmallHealthPowerup : HealthPowerup
    {
        public SmallHealthPowerup(Position position)
            : base(position, 20)
        {
            _powerupType = PowerupType.SmallHealth;
        }
    }
}
