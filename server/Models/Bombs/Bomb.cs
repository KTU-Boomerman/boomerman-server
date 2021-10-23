using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public abstract class Bomb : IDataTransferable<BombDTO>
    {
        protected Position _position;
        protected BombType _bombType;
        public abstract void Explode();
        public abstract void SetPosition(Position position);
        public abstract Bomb Clone();
        public abstract Bomb DeepClone();

        public BombDTO ToDTO()
        {
            return new BombDTO()
            {
                Position = _position.ToDTO(),
                BombType = _bombType
            };
        }
    }
}
