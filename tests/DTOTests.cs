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
    }
}
