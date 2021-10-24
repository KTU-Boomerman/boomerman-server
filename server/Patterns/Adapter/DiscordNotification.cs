using System;

/**
 * The Adapter is a class that links the Target interface and the Adaptee class.
 * In this case, it allows the application to send notifications using Discord API.
 */
namespace BoomermanServer.Patterns.Adapter
{
    public class DiscordNotification : Notification
    {
        private DiscordApi _api;
        public DiscordNotification(DiscordApi api)
        {
            this._api = api;
        }

        public void Send(string title, string message)
        {
            var content = $"**{title}**\n{message}\n\n{DateTime.Now.ToString("yyyy-MM-dd HH:mm")}";

            _api.SendMessage(content);
        }
    }
}