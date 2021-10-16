using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public class BigBombCountPowerup : BombCountPowerup
    {
        public BigBombCountPowerup(Position position)
            : base(position, 2) { }

        public override Powerup Clone()
        {
            return MemberwiseClone() as BigBombCountPowerup;
        }
    }
}
