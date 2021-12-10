using System;
using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Game;
using BoomermanServer.Patterns.Interpreter;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class CommandHandler : AbstractChatHandler
	{
        private IPlayerManager _playerManager;

        public CommandHandler(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }
		public override Message Handle(Message message)
		{
            if (message.Text.StartsWith("/"))
            {
                var text = message.Text.Substring(1);
                var parts = text.Split(' ');

                var command = "";
                var args = parts.Skip(1).ToList();

                if (parts.Length > 0)
                {
                    command = parts[0];
                }

                var interpreter = new CommandExpression(
                    new AttributeExpression(new ValueExpression(args), command));

                return interpreter.interpret(new MessageContext(_playerManager, message));
            }

			return base.Handle(message);
		}
	}
}
