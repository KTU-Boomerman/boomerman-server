namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public abstract class AbstractChatHandler: IChatHandler
    {
        private IChatHandler _nextHandler;

        public IChatHandler SetNext(IChatHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual Message Handle(Message message)
        {
            if (_nextHandler != null)
                return _nextHandler.Handle(message);
        
            return message;
        }   
    }
}