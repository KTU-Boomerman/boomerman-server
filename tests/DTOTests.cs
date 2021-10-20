using BoomermanServer.Data;
using BoomermanServer.Game;
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
    }
}
