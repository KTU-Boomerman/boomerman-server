using BoomermanServer.Game;
using BoomermanServer.Patterns.Factories;
using Xunit;

namespace tests
{
    public class BigPowerupFactoryTests
    {
        [Fact]
        public void CreateBombPowerup()
        {
            var factory = new BigPowerupFactory();
            var powerup = factory.CreateBombCountPowerup(new Position(0, 0));
            Assert.NotNull(powerup);
        }

        [Fact]
        public void CreateHealthPowerup()
        {
            var factory = new BigPowerupFactory();
            var powerup = factory.CreateHealthPowerup(new Position(0, 0));
            Assert.NotNull(powerup);
        }

        [Fact]
        public void CreateSpeedPowerup()
        {
            var factory = new BigPowerupFactory();
            var powerup = factory.CreateSpeedPowerup(new Position(0, 0));
            Assert.NotNull(powerup);
        }
    }
}
