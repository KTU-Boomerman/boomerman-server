using BoomermanServer.Game;

namespace BoomermanServer.Models
{
    public abstract class Bomb
    {
        protected Position _position;
        public abstract void Explode(); // KABOOM
        public Bomb(Position position)
        {
            _position = position;
        }
    }
}
