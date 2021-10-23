using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Speed
{
    public class SmallSpeedPowerup : SpeedPowerup
    {
        public SmallSpeedPowerup(Position position)
            : base(position, 1.2) { }
    }
}
