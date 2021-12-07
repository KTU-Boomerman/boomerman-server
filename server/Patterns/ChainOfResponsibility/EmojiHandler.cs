using System.Collections.Generic;
using System.Linq;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class EmojiHandler : AbstractChatHandler
	{
        Dictionary<string, string> emoticons = new Dictionary<string, string>{ 
            {":)", "😃"}, 
            {":(", "😔"},
            {"haha", "🤣"},
            {"cold", "🥶"},
            {"icecream", "🍦"},
            {"poop", "💩"}
        };

		public override Message Handle(Message message)
		{
            if (emoticons.Keys.Any(message.Text.Contains))
            {
                message.Text = emoticons.Aggregate(message.Text,
                    (text, emoticon) => text.Replace(emoticon.Key, emoticon.Value));
                
                return base.Handle(message);
            }

			return base.Handle(message);
		}
	}
}
