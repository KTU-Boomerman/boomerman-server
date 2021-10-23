using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;
using Xunit;

namespace tests
{
    public class PrototypeTests
    {
        [Fact]
        public void RegularBombShallowEquality()
        {
            var bomb = new RegularBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.Clone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void WaveBombShallowEquality()
        {
            var bomb = new WaveBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.Clone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void PulseBombShallowEquality()
        {
            var bomb = new PulseBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.Clone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void BoomerangBombShallowEquality()
        {
            var bomb = new BoomerangBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.Clone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void RegularBombDeepEquality()
        {
            var bomb = new RegularBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.DeepClone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void WaveBombDeepEquality()
        {
            var bomb = new WaveBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.DeepClone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void PulseBombDeepEquality()
        {
            var bomb = new PulseBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.DeepClone();
            Assert.Equal(bomb, clone);
        }

        [Fact]
        public void BoomerangBombDeepEquality()
        {
            var bomb = new BoomerangBomb();
            bomb.SetPosition(new Position(0, 0));
            var clone = bomb.DeepClone();
            Assert.Equal(bomb, clone);
        }
    }
}
