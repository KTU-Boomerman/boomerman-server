using System.Collections.Generic;
using BoomermanServer.Models;
using BoomermanServer.Models.Bombs;
using Xunit;

namespace tests
{
    public class ExplosionTests
    {
        [Fact]
        public void CreateExplosion()
        {
            var bomb = new RegularBomb();
            var pendingExplosions = new Queue<Explosion>();
            pendingExplosions.Enqueue(new Explosion(bomb, pendingExplosions));
            Assert.Single(pendingExplosions);
        }

        [Fact]
        public void InitiateExplosion()
        {
            var bomb = new RegularBomb();
            var pendingExplosions = new Queue<Explosion>();
            pendingExplosions.Enqueue(new Explosion(bomb, pendingExplosions));
            pendingExplosions.Peek().Explode(null, null);
            Assert.Empty(pendingExplosions);
        }
    }
}
