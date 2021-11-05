using System;
using BoomermanServer.Game;
using BoomermanServer.Models.Explosions;

namespace BoomermanServer.Patterns.Strategy
{
    public interface IExplosionStrategy
    {
        Explosions GetExplosions(Position position, TimeSpan delay);
    }
}