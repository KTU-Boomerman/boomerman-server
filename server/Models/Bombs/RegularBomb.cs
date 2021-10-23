using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    [Serializable]
    public class RegularBomb : Bomb
    {
        public RegularBomb()
        {
            _bombType = BombType.Regular;
        }

        public override Bomb Clone()
        {
            return MemberwiseClone() as RegularBomb;
        }

        public override Bomb DeepClone()
        {
#pragma warning disable SYSLIB0011
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream) as RegularBomb;
            }
        }

        public override void Explode()
        {
            Console.WriteLine("Regular explosion"); // TODO: Add actual explosion logic
        }

        public override void SetPosition(Position position)
        {
            _position = position;
        }
    }
}
