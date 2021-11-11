using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public class BigBombCountPowerup : BombCountPowerup
    {
        public BigBombCountPowerup(Position position)
            : base(position, 2)
        {
            _powerupType = PowerupType.BigBomb;
        }
    }
}
