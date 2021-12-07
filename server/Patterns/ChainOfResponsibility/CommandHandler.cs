using System;
using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class CommandHandler : AbstractChatHandler
	{
        Dictionary<string, Func<string[], Message, Message>> commands;

        public CommandHandler(IPlayerManager playerManager) : base()
        {
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
                playerManager.GetPlayer(message.PlayerID).Name = newName;
                message.PlayerName = newName;

                return message;
            });
        }
		public override Message Handle(Message message)
		{
            if (message.Text.StartsWith("/"))
            {
                var text = message.Text.Substring(1);
                var parts = text.Split(' ');

                var command = parts[0];
                var args = parts.Skip(1).ToArray();

                if (commands.ContainsKey(command)) {
                    return commands[command](args, message);
                }
                else
                {
                    return new Message {
                        PlayerID = "Server",
                        PlayerName = "Server",
                        Text = "Unknown command: " + command
                    };
                }
            }

			return base.Handle(message);
		}


	}
}
