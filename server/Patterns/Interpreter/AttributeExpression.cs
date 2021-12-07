using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public class AttributeExpression : Expression
    {
        private ValueExpression _valueExpression;
        private string _command;

        public AttributeExpression(ValueExpression valueExpression, string command)
        {
            _valueExpression = valueExpression;
            _command = command;
        }
        
        public Message interpret(MessageContext messageContext)
        {
            messageContext.Command = _command;
            return _valueExpression.interpret(messageContext);
        }
    }
}