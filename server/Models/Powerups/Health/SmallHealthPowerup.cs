using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Health
{
    public class SmallHealthPowerup : HealthPowerup
    {
        public SmallHealthPowerup(Position position)
            : base(position, 20) { }

        public override Powerup Clone()
        {
            return MemberwiseClone() as SmallHealthPowerup;
        }

        public override Powerup DeepClone()
        {
#pragma warning disable SYSLIB0011
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream) as SmallHealthPowerup;
            }
        }
    }
}
