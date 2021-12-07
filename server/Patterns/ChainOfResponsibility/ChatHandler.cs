using BoomermanServer.Game;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class ChatHandler
    {
        private IChatHandler _mainHandler;

        public ChatHandler(IPlayerManager playerManager)
        {
            var commandHandler = new CommandHandler(playerManager);
            var profanityHandler = new ProfanityHandler();
            var emojiHandler = new EmojiHandler();
            var playerNameHandler = new PlayerNameHandler(playerManager);

            emojiHandler.SetNext(playerNameHandler);
            profanityHandler.SetNext(emojiHandler);
            commandHandler.SetNext(profanityHandler);

            _mainHandler = commandHandler;
        }

        public Message Handle(Message message)
        {
            return _mainHandler.Handle(message);
        }
    }
}
