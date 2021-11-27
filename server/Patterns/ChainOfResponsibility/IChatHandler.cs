namespace BoomermanServer.Patterns.ChainOfResponsibility
{
	public interface IChatHandler
	{
        IChatHandler SetNext(IChatHandler handler);
        Message Handle(Message Message);
	}
}