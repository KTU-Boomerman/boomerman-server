using BoomermanServer.Game;
using Xunit;

namespace tests
{
    public class PlayerManagerTests
    {
        private const string _id = "AAA";

        [Fact]
        public void AddPlayer()
        {
            var manager = new PlayerManager();
            var countBefore = manager.GetPlayerCount();
            manager.AddPlayer(_id);
            Assert.True(countBefore < manager.GetPlayerCount());
        }

        [Fact]
        public void GetPlayer()
        {
            var manager = new PlayerManager();
            manager.AddPlayer(_id);
            var player = manager.GetPlayer(_id);
            Assert.NotNull(player);
        }

        [Fact]
        public void GetPlayerList()
        {
            var manager = new PlayerManager();
            var list = manager.GetPlayers();
            Assert.NotNull(list);
        }

        [Fact]
        public void MovePlayer()
        {
            var manager = new PlayerManager();
            manager.AddPlayer(_id);
            var player = manager.GetPlayer(_id);
            var position = player.Position;
            manager.MovePlayer(_id, new Position(position.X + 1, position.Y + 1));
            Assert.NotEqual(position, player.Position);
        }

        [Fact]
        public void RemovePlayer()
        {
            var manager = new PlayerManager();
            manager.AddPlayer(_id);
            var countBefore = manager.GetPlayerCount();
            manager.RemovePlayer(_id);
            Assert.True(countBefore > manager.GetPlayerCount());
        }
    }
}
