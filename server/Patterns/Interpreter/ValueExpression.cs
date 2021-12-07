using System.Collections.Generic;
using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public class ValueExpression : Expression
    {
        private List<string> _value;

        public ValueExpression(List<string> value)
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