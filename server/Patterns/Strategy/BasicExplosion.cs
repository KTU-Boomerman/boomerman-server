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
			return new Explosions(new Explosion(position, delay));
		}
	}
}