using BoomermanServer.Game;
using BoomermanServer.Patterns.Facade;
using Xunit;

namespace tests
{
    public class GameManagerTests
    {
        [Fact]
        public void MinPlayers()
        {
            var manager = new GameManager();
            var facade = new ManagerFacade(manager, null, null);
            Assert.Equal(1, facade.GetMinPlayers());
        }

        [Fact]
        public void GameInProgress()
        {
            var manager = new GameManager();
            var facade = new ManagerFacade(manager, null, null);
            facade.StartGame();
            Assert.Equal(GameState.GameInProgress, facade.GameState);
        }
    }
}
