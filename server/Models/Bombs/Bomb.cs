using System;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    [Serializable]
    public abstract class Bomb : IDataTransferable<BombDTO>
    {

        protected Position _position;
        protected BombType _bombType;

        private Player _owner;
        public abstract void Explode();
        public abstract void SetPosition(Position position);
        public abstract Bomb Clone();
        public abstract Bomb DeepClone();

        public Position Position => _position;

        public Player Owner
        {
            get => _owner;
            set => _owner = value;
        }

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
            if (obj is Bomb)
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
    }
}
