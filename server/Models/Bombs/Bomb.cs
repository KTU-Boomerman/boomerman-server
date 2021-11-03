using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    [Serializable]
    public abstract class Bomb : IDataTransferable<BombDTO>
    {
        public Position _position { get; protected set; }
        protected BombType _bombType;
        public abstract void Explode();
        public abstract void SetPosition(Position position);
        
        public BombDTO ToDTO()
        {
            return new BombDTO()
            {
                Position = _position.ToDTO(),
                BombType = _bombType
            };
        }

        public override bool Equals(object obj)
        {
            if(obj is Bomb)
            {
                var bomb = obj as Bomb;
                return bomb._bombType == _bombType && bomb._position.Equals(_position);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual Bomb Clone()
        {
            return MemberwiseClone() as BoomerangBomb;
        }

        public virtual Bomb DeepClone()
        {
            #pragma warning disable SYSLIB0011
            using (var memoryStream = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return formatter.Deserialize(memoryStream) as BoomerangBomb;
            }
        }
    }
}
