using System;
using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Models;

namespace BoomermanServer.Patterns.Strategy
{
	public class WaveExplosion : IExplosionStrategy
	{
		public List<Explosion> GetExplosions(Position position, TimeSpan delay)
		{
			var tile = new Position(32, 32);

			var explosions = new List<Explosion>();

			explosions.Add(new Explosion(position, delay));

			// TODO: only add explosions if they can go further and are not facing walls
			for (int i = 1; i < 4; i++)
			{
				var newDelay = delay.Add(delay * i);

				// add explosion to the right
				explosions.Add(new Explosion(new Position(position.X + tile.X * i, position.Y), newDelay));

				// add explosion to the top
				explosions.Add(new Explosion(new Position(position.X, position.Y + tile.Y * i), newDelay));

				// add explosion to the left
				explosions.Add(new Explosion(new Position(position.X - tile.X * i, position.Y), newDelay));

				// add explosion to the bottom
				explosions.Add(new Explosion(new Position(position.X, position.Y - tile.Y * i), newDelay));
			}


			return explosions;	
		}
	}
}