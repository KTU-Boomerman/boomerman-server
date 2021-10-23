using BoomermanServer.Game;
using BoomermanServer.Models.Powerups.Appliers;
using BoomermanServer.Models.Powerups.BombCount;
using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;
using Xunit;

namespace tests
{
    public class BridgeTests
    {
        private const string _id = "AAA";

        [Fact]
        public void ApplySpeed()
        {
            var position = new Position(0, 0);
            var player = new Player(_id, position);
            var applier = new ConcreteApplier();
            applier.Powerup = new SmallSpeedPowerup(position);
            var speedBefore = player.Speed;
            applier.ApplyPowerup(player);
            Assert.True(speedBefore < player.Speed);
        }

        [Fact]
        public void ApplyHealth()
        {
            var position = new Position(0, 0);
            var player = new Player(_id, position);
            var applier = new ConcreteApplier();
            applier.Powerup = new SmallHealthPowerup(position);
            var healthBefore = player.Health;
            applier.ApplyPowerup(player);
            Assert.True(healthBefore < player.Health);
        }

        [Fact]
        public void ApplyBombCount()
        {
            var position = new Position(0, 0);
            var player = new Player(_id, position);
            var applier = new ConcreteApplier();
            applier.Powerup = new SmallBombCountPowerup(position);
            var bombsBefore = player.MaxBombCount;
            applier.ApplyPowerup(player);
            Assert.True(bombsBefore < player.MaxBombCount);
        }
    }
}
