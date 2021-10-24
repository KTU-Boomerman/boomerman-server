using Discord;
using Discord.Webhook;

/**
 * The Adaptee is some useful class, incompatible with the Target interface. You
 * can't just go in and change the code of the class to follow the Target
 * interface, since the code might be provided by a 3rd-party library.
 */
namespace BoomermanServer.Patterns.Adapter
{
    public class DiscordApi
    {
        private DiscordWebhook hook;

        public DiscordApi()
        {
            hook = new DiscordWebhook {
                Url = "https://discord.com/api/webhooks/901896663449346079/v-xwP6JuFSmMrbKVyrlEDUpbxKx5aePGEPik2VJR2IKTT3AIJ3QnOwtRlM5JWNXy2zXF" 
            };
        }

        public void SendMessage(string content)
        {
            DiscordMessage message = new DiscordMessage();
            message.Content = content;

            hook.Send(message);
        }
    }
}