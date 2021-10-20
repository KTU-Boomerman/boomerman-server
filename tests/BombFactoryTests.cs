using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Patterns.Factories;
using Xunit;

namespace tests
{
    public class BombFactoryTests
    {
        [Fact]
        public void CreateWaveBomb()
        {
            var bombFactory = new BombFactory();
            var bomb = bombFactory.CreateBomb(BombType.Wave, new Position(0, 0));
            Assert.NotNull(bomb);
        }

        [Fact]
        public void CreatePulseBomb()
        {
            var bombFactory = new BombFactory();
            var bomb = bombFactory.CreateBomb(BombType.Pulse, new Position(0, 0));
            Assert.NotNull(bomb);
        }

        [Fact]
        public void CreateRegularBomb()
        {
            var bombFactory = new BombFactory();
            var bomb = bombFactory.CreateBomb(BombType.Regular, new Position(0, 0));
            Assert.NotNull(bomb);
        }

        [Fact]
        public void CreateBoomerangBomb()
        {
            var bombFactory = new BombFactory();
            var bomb = bombFactory.CreateBomb(BombType.Boomerang, new Position(0, 0));
            Assert.NotNull(bomb);
        }
    }
}
