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
            var healthBefore = player.Lives;
            applier.ApplyPowerup(player);
            Assert.True(healthBefore < player.Lives);
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

        [Fact]
        public void RemoveSpeed()
        {
            var position = new Position(0, 0);
            var player = new Player(_id, position);
            var remover = new ConcreteRemover();
            remover.Powerup = new SmallSpeedPowerup(position);
            var speedBefore = player.Speed;
            remover.RemovePowerup(player);
            Assert.True(speedBefore > player.Speed);
        }

        [Fact]
        public void RemoveHealth()
        {
            var position = new Position(0, 0);
            var player = new Player(_id, position);
            var remover = new ConcreteRemover();
            remover.Powerup = new SmallHealthPowerup(position);
            var healthBefore = player.Lives;
            remover.RemovePowerup(player);
            Assert.True(healthBefore > player.Lives);
        }

        [Fact]
        public void RemoveBombCount()
        {
            var position = new Position(0, 0);
            var player = new Player(_id, position);
            var remover = new ConcreteRemover();
            remover.Powerup = new SmallBombCountPowerup(position);
            var bombsBefore = player.MaxBombCount;
            remover.RemovePowerup(player);
            Assert.True(bombsBefore > player.MaxBombCount);
        }
    }
}
