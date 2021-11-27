using BoomermanServer.Game;
using BoomermanServer.Patterns.Facade;
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
            var facade = new ManagerFacade(null, manager, null);
            var countBefore = facade.GetPlayerCount();
            facade.AddPlayer(_id);
            Assert.True(countBefore < facade.GetPlayerCount());
        }

        [Fact]
        public void GetPlayer()
        {
            var manager = new PlayerManager();
            var facade = new ManagerFacade(null, manager, null);
            facade.AddPlayer(_id);
            var player = facade.GetPlayer(_id);
            Assert.NotNull(player);
        }

        [Fact]
        public void GetPlayerList()
        {
            var manager = new PlayerManager();
            var facade = new ManagerFacade(null, manager, null);
            var list = facade.GetPlayers();
            Assert.NotNull(list);
        }

        [Fact]
        public void MovePlayer()
        {
            var manager = new PlayerManager();
            var facade = new ManagerFacade(null, manager, null);
            facade.AddPlayer(_id);
            var player = facade.GetPlayer(_id);
            var position = player.Position;
            facade.MovePlayer(_id, new Position(position.X + 1, position.Y + 1));
            Assert.NotEqual(position, player.Position);
        }

        [Fact]
        public void RemovePlayer()
        {
            var manager = new PlayerManager();
            var facade = new ManagerFacade(null, manager, null);
            facade.AddPlayer(_id);
            var countBefore = facade.GetPlayerCount();
            facade.RemovePlayer(_id);
            Assert.True(countBefore > facade.GetPlayerCount());
        }
    }
}
