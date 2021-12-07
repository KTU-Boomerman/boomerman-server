using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public class CommandExpression : Expression
    {
        private AttributeExpression _attributeExpression;

        public CommandExpression(AttributeExpression attributeExpression)
        {
            _attributeExpression = attributeExpression;
        }
        
        public Message interpret(MessageContext messageContext)
        {
            messageContext.IsCommand = true;
            return _attributeExpression.interpret(messageContext);
        }
    }
}