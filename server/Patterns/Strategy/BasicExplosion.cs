using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Game;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;

namespace BoomermanServer.Patterns.Strategy
{
	public class BasicExplosion : IExplosionStrategy
	{
		public Explosions GetExplosions(Position position, TimeSpan delay)
		{
			var tile = Position.Tile;
			var origin = new Explosion(position, delay); 
			var explosions = new Explosions(origin);

			explosions.AddExplosion(new Explosion(new Position(position.X + tile.X, position.Y), delay));

			// add explosion to the top
			explosions.AddExplosion(new Explosion(new Position(position.X, position.Y + tile.Y), delay));

			// add explosion to the left
			explosions.AddExplosion(new Explosion(new Position(position.X - tile.X, position.Y), delay));

			// add explosion to the bottom
			explosions.AddExplosion(new Explosion(new Position(position.X, position.Y - tile.Y), delay));

			return explosions;	
		}
	}
}