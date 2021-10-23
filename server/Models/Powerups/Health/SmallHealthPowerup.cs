using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Health
{
    public class SmallHealthPowerup : HealthPowerup
    {
        public SmallHealthPowerup(Position position)
            : base(position, 20) { }
    }
}
