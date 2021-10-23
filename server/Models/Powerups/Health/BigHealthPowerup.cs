using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Health
{
    public class BigHealthPowerup : HealthPowerup
    {
        public BigHealthPowerup(Position position)
            : base(position, 40) { }
    }
}
