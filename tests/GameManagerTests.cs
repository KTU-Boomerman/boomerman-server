using BoomermanServer.Game;
using Xunit;

namespace tests
{
    public class GameManagerTests
    {
        [Fact]
        public void MinPlayers()
        {
            var manager = new GameManager();
            Assert.Equal(1, manager.GetMinPlayers());
        }

        [Fact]
        public void GameInProgress()
        {
            var manager = new GameManager();
            manager.StartGame();
            Assert.Equal(GameState.GameInProgress, manager.GameState);
        }
    }
}
