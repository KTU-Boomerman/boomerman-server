namespace BoomermanServer.Patterns.Decorator
{
    public abstract class Decorator
    {
        public Decorator Component { get; set; }
        public abstract void Execute();
    }
}
