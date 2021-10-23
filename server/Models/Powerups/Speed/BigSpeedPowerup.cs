using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Speed
{
    public class BigSpeedPowerup : SpeedPowerup
    {
        public BigSpeedPowerup(Position position)
            : base(position, 1.4) { }
    }
}
