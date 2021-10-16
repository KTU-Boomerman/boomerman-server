using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Health
{
    public class BigHealthPowerup : HealthPowerup
    {
        public BigHealthPowerup(Position position)
            : base(position, 40) { }

        public override Powerup Clone()
        {
            return MemberwiseClone() as BigHealthPowerup;
        }
    }
}
