using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public interface Expression
    {
        public Message interpret(MessageContext messageContext);
    }
}