using System;
using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Game;
using BoomermanServer.Patterns.Interpreter;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class CommandHandler : AbstractChatHandler
	{
        Dictionary<string, Func<string[], Message, Message>> commands;
        private IPlayerManager _playerManager;

        public CommandHandler(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
            commands = new Dictionary<string, Func<string[], Message, Message>>();
            
            commands.Add("help", (args, message) =>
            {
                return new Message {
                    PlayerID = "Server",
                    PlayerName = "Server",
                    Text = "Available commands: " + string.Join(", ", commands.Keys)
                };
            });

            commands.Add("name", (args, message) =>
            {
                var newName = args[0];                
                _playerManager.GetPlayer(message.PlayerID).Name = newName;
                message.PlayerName = newName;

                return message;
            });

            commands.Add("msg", (args, message) =>
            {
                var from = playerManager.GetPlayer(message.PlayerID);
                var to = playerManager.GetPlayers().FirstOrDefault(p => p.Name == args[0]);
                if(from != null && to != null && args.Length > 1)
                {
                    message.PlayerID = "Server";
                    message.PlayerName = "Server";
                    message.Text = from.Send(to.ID, string.Join(" ", args.Skip(1)));
                }
                return message;
            });
        }
		public override Message Handle(Message message)
		{
            if (message.Text.StartsWith("/"))
            {
                var text = message.Text.Substring(1);
                var parts = text.Split(' ', 2);

                var command = "";
                var args = "";

                if (parts.Length > 0)
                {
                    command = parts[0];
                }
                if (parts.Length > 1)
                {
                    args = parts[1];
                }
                
                var interpreter = new CommandExpression(
                    new AttributeExpression(new ValueExpression(args), command));

                return interpreter.interpret(new MessageContext(_playerManager, message));
            }

			return base.Handle(message);
		}
	}
}
