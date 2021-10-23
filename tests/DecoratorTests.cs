using BoomermanServer.Data;
using BoomermanServer.Patterns.Decorator;
using Xunit;

namespace tests
{
    public class DecoratorTests
    {
        [Fact]
        public void TestExecution()
        {
            var decorator1 = new BombDecorator(BombType.Regular);
            var decorator2 = new BombDecorator(BombType.Pulse);
            decorator2.Component = decorator1;
            decorator2.Execute();
            Assert.Equal(2, decorator2.Bombs.Count);
        }
    }
}
