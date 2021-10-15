using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models
{
    public abstract class Bomb : IDataTransferable<BombDTO>
    {
        protected Position _position;
        public abstract void Explode(); // KABOOM

        public BombDTO ToDTO()
        {
            return new BombDTO()
            {
                Position = _position.ToDTO()
            };
        }

        public Bomb(Position position)
        {
            _position = position;
        }
    }
}
