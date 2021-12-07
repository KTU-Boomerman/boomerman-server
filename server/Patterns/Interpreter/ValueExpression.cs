using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public class ValueExpression : Expression
    {
        private string _value;

        public ValueExpression(string value)
        {
            _value = value;
        }


        public Message interpret(MessageContext messageContext)
        {
            messageContext.Value = _value;
            return messageContext.execute();
        }
    }
}