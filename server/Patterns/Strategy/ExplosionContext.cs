using System;
using BoomermanServer.Game;
using BoomermanServer.Models.Explosions;

namespace BoomermanServer.Patterns.Strategy
{
    public class ExplosionContext
    {
        private IExplosionStrategy _strategy;

        public ExplosionContext(IExplosionStrategy strategy)
        {
            _strategy = strategy;
        }

        public void SetStrategy(IExplosionStrategy strategy)
        {
            _strategy = strategy;
        }

        public Explosions GetExplosions(Position position, TimeSpan delay)
        {
            return _strategy.GetExplosions(position, delay);
        }
    }
}