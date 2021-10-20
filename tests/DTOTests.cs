using boomerman_server.Models.Walls;
using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;
using BoomermanServer.Models.Powerups.BombCount;
using BoomermanServer.Models.Powerups.Health;
using BoomermanServer.Models.Powerups.Speed;
using Xunit;

namespace tests
{
    public class DTOTests
    {
        [Fact]
        public void PlayerToDTO()
        {
            var player = new Player("A", new Position(0, 0));
            var dto = player.ToDTO();
            Assert.NotNull(dto);
        }

        [Fact]
        public void PositionFromDTO()
        {
            var dto = new PositionDTO
            {
                X = 0,
                Y = 0
            };
            var position = new Position(dto);
            Assert.NotNull(position);
        }

        [Fact]
        public void BombToDTO()
        {
            var bomb = new RegularBomb();
            bomb.SetPosition(new Position(0, 0));
            var dto = bomb.ToDTO();
            Assert.NotNull(dto);
        }

        [Fact]
        public void WallToDTO()
        {
            var wall = new Wall();
            wall.Position = new Position(0, 0);
            var dto = wall.ToDTO();
            Assert.NotNull(dto);
        }

        [Fact]
        public void SpeedPowerupToDTO()
        {
            var powerup = new SmallSpeedPowerup(new Position(0, 0));
            var dto = powerup.ToDTO();
            Assert.NotNull(dto);
        }

        [Fact]
        public void HealthPowerupToDTO()
        {
            var powerup = new SmallHealthPowerup(new Position(0, 0));
            var dto = powerup.ToDTO();
            Assert.NotNull(dto);
        }

        [Fact]
        public void BombPowerupToDTO()
        {
            var powerup = new SmallBombCountPowerup(new Position(0, 0));
            var dto = powerup.ToDTO();
            Assert.NotNull(dto);
        }
    }
}
