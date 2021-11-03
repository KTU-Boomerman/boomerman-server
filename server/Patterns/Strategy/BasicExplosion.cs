using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Game;
using BoomermanServer.Models;

namespace BoomermanServer.Patterns.Strategy
{
	public class BasicExplosion : IExplosionStrategy
	{
		public List<Explosion> GetExplosions(Position position, TimeSpan delay)
		{
			return new List<Explosion>()
            {
                new Explosion(position, delay)
            };
		}
	}
}