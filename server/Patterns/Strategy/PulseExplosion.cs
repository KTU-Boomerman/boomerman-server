using System;
using BoomermanServer.Game;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;

namespace BoomermanServer.Patterns.Strategy
{
    public class PulseExplosion : IExplosionStrategy
    {
        public Explosions GetExplosions(Position position, TimeSpan delay, Player owner)
        {
            var tile = Position.Tile;
            var origin = new Explosion(position, delay, owner); 
            var explosions = new Explosions(origin);
            var pulseDelay = TimeSpan.FromMilliseconds(1500);

            for (int i = 1; i < 4; i++)
            {
                var newDelay = delay.Add(pulseDelay * (i-1));

                if (i>1)
                    explosions.AddExplosion(new Explosion(new Position(position.X , position.Y), newDelay, owner));

                // add explosion to the right
                explosions.AddExplosion(new Explosion(new Position(position.X + tile.X, position.Y), newDelay, owner));

                // add explosion to the top
                explosions.AddExplosion(new Explosion(new Position(position.X, position.Y + tile.Y), newDelay, owner));

                // add explosion to the left
                explosions.AddExplosion(new Explosion(new Position(position.X - tile.X, position.Y), newDelay, owner));

                // add explosion to the bottom
                explosions.AddExplosion(new Explosion(new Position(position.X, position.Y - tile.Y), newDelay, owner));
                
                
            }

            return explosions;	
        }
    }
}