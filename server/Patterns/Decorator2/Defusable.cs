using System;

using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Decorator2
{
    public class Defusable : BombDecorator
    {
        private bool _isDefused;

        public Defusable(Bomb bomb) : base(bomb) {
            _isDefused = false;
        }

        public override void Explode()
        {
            if (!_isDefused)
            {
                _bomb.Explode();
            }
            else
            {
                Console.WriteLine("Bomb already defused!");
            }
        }

        public void Defuse()
        {
            _isDefused = true;
        }
	}
}