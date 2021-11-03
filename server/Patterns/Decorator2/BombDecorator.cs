using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Decorator2
{
	public abstract class BombDecorator : Bomb
	{
        protected Bomb _bomb;

        public BombDecorator(Bomb bomb)
        {
            _bomb = bomb;
        }

        public override void SetPosition(Position position) 
        {
            _bomb.SetPosition(position);
        }

        public override void Explode() 
        {
            _bomb.Explode();
        }
	}
}