using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class PulseBomb : Bomb
    {
        public PulseBomb()
        {
            _bombType = BombType.Pulse;
        }
        public override void SetPosition(Position position)
        {
            _position = position;
        }

        public override void Explode()
        {
            Console.WriteLine("Pulse explosion"); // TODO: Add actual explosion logic
        }

        public override Bomb Clone()
        {
            return MemberwiseClone() as PulseBomb;
        }

        public override Bomb DeepClone()
        {
#pragma warning disable SYSLIB0011
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream) as PulseBomb;
            }
        }
    }
}
