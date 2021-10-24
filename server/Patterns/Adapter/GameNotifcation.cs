using Microsoft.AspNetCore.SignalR;

/**
 * Here's an example of the existing class that follows the Target interface.
 */
namespace BoomermanServer.Patterns.Adapter
{
    public class GameNotifcation : Notification
    {
        private IClientProxy _client;

        public GameNotifcation(IClientProxy client)
        {
            _client = client;
        }

        public void Send(string title, string message)
        {
            _client.SendAsync("Notification", title, message);
        }
    }
}