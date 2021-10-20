using BoomermanServer.Game;
using BoomermanServer.Patterns.Factories;
using Xunit;

namespace tests
{
    public class SmallPowerupFactoryTests
    {
        [Fact]
        public void CreateBombPowerup()
        {
            var factory = new SmallPowerupFactory();
            var powerup = factory.CreateBombCountPowerup(new Position(0, 0));
            Assert.NotNull(powerup);
        }

        [Fact]
        public void CreateHealthPowerup()
        {
            var factory = new SmallPowerupFactory();
            var powerup = factory.CreateHealthPowerup(new Position(0, 0));
            Assert.NotNull(powerup);
        }

        [Fact]
        public void CreateSpeedPowerup()
        {
            var factory = new SmallPowerupFactory();
            var powerup = factory.CreateSpeedPowerup(new Position(0, 0));
            Assert.NotNull(powerup);
        }
    }
}
