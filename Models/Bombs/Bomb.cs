using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Bombs
{
    public abstract class Bomb : IDataTransferable<BombDTO>
    {
        protected Position _position;
        protected BombType _bombType;
        public abstract void Explode(); // KABOOM

        public BombDTO ToDTO()
        {
            return new BombDTO()
            {
                Position = _position.ToDTO(),
                BombType = _bombType
            };
        }

        public Bomb(Position position)
        {
            _position = position;
        }
    }
}
