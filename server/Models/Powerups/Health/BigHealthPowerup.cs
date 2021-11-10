using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Health
{
    public class BigHealthPowerup : HealthPowerup
    {
        public BigHealthPowerup(Position position)
            : base(position, 40)
        {
            _powerupType = PowerupType.BigHealth;
        }
    }
}
