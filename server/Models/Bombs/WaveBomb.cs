using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class WaveBomb : Bomb
    {
        public WaveBomb()
        {
            _bombType = BombType.Wave;
        }

        public override Bomb Clone()
        {
            return MemberwiseClone() as WaveBomb;
        }

        public override Bomb DeepClone()
        {
#pragma warning disable SYSLIB0011
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream) as WaveBomb;
            }
        }

        public override void Explode()
        {
            Console.WriteLine("Wave explosion"); // TODO: Add actual explosion logic
        }

        public override void SetPosition(Position position)
        {
            _position = position;
        }
    }
}
