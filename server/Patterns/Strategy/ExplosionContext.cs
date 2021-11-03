using System;
using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Models;

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

        public List<Explosion> GetExplosions(Position position, TimeSpan delay)
        {
            return _strategy.GetExplosions(position, delay);
        }
    }
}