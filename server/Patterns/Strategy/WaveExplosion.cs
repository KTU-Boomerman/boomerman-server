using System;
using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;

namespace BoomermanServer.Patterns.Strategy
{
	public class WaveExplosion : IExplosionStrategy
	{
		public Explosions GetExplosions(Position position, TimeSpan delay)
		{
			var tile = new Position(32, 32);
			var origin = new Explosion(position, delay); 
			var explosions = new Explosions(origin);
			var waveDelay = TimeSpan.FromMilliseconds(500);

			for (int i = 1; i < 4; i++)
			{
				var newDelay = delay.Add(waveDelay * i);

				// add explosion to the right
				explosions.AddExplosion(new Explosion(new Position(position.X + tile.X * i, position.Y), newDelay));

				// add explosion to the top
				explosions.AddExplosion(new Explosion(new Position(position.X, position.Y + tile.Y * i), newDelay));

				// add explosion to the left
				explosions.AddExplosion(new Explosion(new Position(position.X - tile.X * i, position.Y), newDelay));

				// add explosion to the bottom
				explosions.AddExplosion(new Explosion(new Position(position.X, position.Y - tile.Y * i), newDelay));
			}

			return explosions;	
		}
	}
}