using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Patterns.Command;
using Xunit;

namespace tests
{
    public class CommandPatternTests
    {
        private ImmortalitySetter _immortalitySetter;
        private List<Player> players;

        public CommandPatternTests()
        {
            players = new List<Player>();
            var player1 = new Player("A", new Position(0, 0));
            var player2 = new Player("B", new Position(0, 0));
            players.Add(player1);
            players.Add(player2);
            _immortalitySetter = new ImmortalitySetter(players);
        }

        [Fact]
        public void EnableImmortality()
        {
            _immortalitySetter.SetAttributes();
            var result = players.TrueForAll(player => player.IsImmortal);
            Assert.True(result);
        }

        [Fact]
        public void DisableImmortality()
        {
            _immortalitySetter.Undo();
            var result = players.TrueForAll(player => !player.IsImmortal);
            Assert.True(result);
        }
    }
}
