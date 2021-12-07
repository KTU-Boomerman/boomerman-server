using BoomermanServer.Game;
using BoomermanServer.Patterns.ChainOfResponsibility;

namespace BoomermanServer.Patterns.Interpreter
{
    public class MessageContext
    {
        private IPlayerManager _playerManager;
        private Message _message;
        public bool IsCommand { get; set; }
        public string Value { get; set; }
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
                    _playerManager.GetPlayer(_message.PlayerID).Name = Value;
                    message.PlayerName = "Server";
                    message.PlayerID = "Server";
                    message.Text = $"{_message.PlayerName} name is set to {Value}";
                    return message;
                default:
                    message.PlayerName = "Server";
                    message.PlayerID = "Server";
                    message.Text = "Command not recognized";
                    return message;
            }
        }

    }
}