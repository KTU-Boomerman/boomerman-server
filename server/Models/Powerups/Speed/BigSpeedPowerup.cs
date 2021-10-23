using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Speed
{
    public class BigSpeedPowerup : SpeedPowerup
    {
        public BigSpeedPowerup(Position position)
            : base(position, 1.4) { }
    }
}
