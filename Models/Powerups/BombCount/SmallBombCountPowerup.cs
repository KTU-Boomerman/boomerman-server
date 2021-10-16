using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public class SmallBombCountPowerup : BombCountPowerup
    {
        public SmallBombCountPowerup(Position position)
            : base(position, 1) { }

        public override Powerup Clone()
        {
            return MemberwiseClone() as SmallBombCountPowerup;
        }
    }
}
