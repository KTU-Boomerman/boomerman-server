namespace BoomermanServer.Patterns.Decorator
{
    public abstract class Creator
    {
        public Creator Component { get; set; }
        public abstract void Execute();
    }
}
