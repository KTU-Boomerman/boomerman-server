using System;
using BoomermanServer.Game;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;

namespace BoomermanServer.Patterns.Strategy
{
    public class BoomerangExplosion : IExplosionStrategy
    {
        public Explosions GetExplosions(Position position, TimeSpan delay, Player owner)
        {
            var tile = Position.Tile;
            var origin = new Explosion(position, delay, owner); 
            var explosions = new Explosions(origin);
            var waveDelay = TimeSpan.FromMilliseconds(500);
            var newDelay = delay;
			
            for (int i = 1; i < 3; i++)
            {
                newDelay = newDelay.Add(waveDelay);

                // add explosion to the right
                explosions.AddExplosion(new Explosion(new Position(position.X + tile.X * i, position.Y), newDelay, owner));

                // add explosion to the top
                explosions.AddExplosion(new Explosion(new Position(position.X, position.Y + tile.Y * i), newDelay, owner));

                // add explosion to the left
                explosions.AddExplosion(new Explosion(new Position(position.X - tile.X * i, position.Y), newDelay, owner));

                // add explosion to the bottom
                explosions.AddExplosion(new Explosion(new Position(position.X, position.Y - tile.Y * i), newDelay, owner));
            }
			
            for (int i = 2; i > 0; --i)
            {
                newDelay = newDelay.Add(waveDelay);

                // add explosion to the right
                explosions.AddExplosion(new Explosion(new Position(position.X + tile.X * i, position.Y), newDelay, owner));

                // add explosion to the top
                explosions.AddExplosion(new Explosion(new Position(position.X, position.Y + tile.Y * i), newDelay, owner));

                // add explosion to the left
                explosions.AddExplosion(new Explosion(new Position(position.X - tile.X * i, position.Y), newDelay, owner));

                // add explosion to the bottom
                explosions.AddExplosion(new Explosion(new Position(position.X, position.Y - tile.Y * i), newDelay, owner));
            }
            newDelay = newDelay.Add(waveDelay);
			
            explosions.AddExplosion(new Explosion(new Position(position.X, position.Y), newDelay, owner));

            return explosions;
        }
    }
}