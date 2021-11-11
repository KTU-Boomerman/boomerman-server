using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public class SmallBombCountPowerup : BombCountPowerup
    {
        public SmallBombCountPowerup(Position position)
            : base(position, 1)
        {
            _powerupType = PowerupType.SmallBomb;
        }
    }
}
