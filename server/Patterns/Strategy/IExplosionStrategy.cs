using System;
using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Models;

namespace BoomermanServer.Patterns.Strategy
{
    public interface IExplosionStrategy
    {
        List<Explosion> GetExplosions(Position position, TimeSpan delay);
    }
}