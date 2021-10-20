using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
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

        public override Powerup DeepClone()
        {
#pragma warning disable SYSLIB0011
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream) as BigHealthPowerup;
            }
        }
    }
}
