using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Game;
using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public class MessageContext
    {
        private IPlayerManager _playerManager;
        private Message _message;
        public bool IsCommand { get; set; }
        public List<string> Value { get; set; }
        public string Command { get; set; }

        public MessageContext(IPlayerManager playerManager, Message message)
        {
            _playerManager = playerManager;
            _message = message;
        }

        public Message execute()
        {
            if (!IsCommand) return _message;

            var message = new Message();
            switch (Command)
            {
                case "name" :
                    var newName = string.Join(' ', Value);
                    _playerManager.GetPlayer(_message.PlayerID).Name = newName;
                    message.PlayerName = "Server";
                    message.PlayerID = "Server";
                    message.Text = $"{_message.PlayerName} name is set to {newName}";
                    return message;
                case "msg" :
                    var from = _playerManager.GetPlayer(_message.PlayerID);
                    var to = _playerManager.GetPlayers().FirstOrDefault(p => p.Name == Value[0]);
                    if(from != null && to != null && Value.Count > 1)
                    {
                        message.PlayerID = "Server";
                        message.PlayerName = "Server";
                        message.Text = from.Send(to.ID, string.Join(" ", Value.Skip(1)));
                        return message;
                    }
                    else
                    {
                        message.PlayerID = "Server";
                        message.PlayerName = "Server";
                        message.Text = "Invalid direct message";
                        return message;
                    }
                    
                default:
                    message.PlayerName = "Server";
                    message.PlayerID = "Server";
                    message.Text = "Command not recognized";
                    return message;
            }
        }

    }
}