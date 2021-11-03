using BoomermanServer.Hubs;
using Microsoft.AspNetCore.SignalR;

/**
 * Here's an example of the existing class that follows the Target interface.
 */
namespace BoomermanServer.Patterns.Adapter
{
    public class GameNotifcation : Notification
    {
        private IHubContext<GameHub, IGameHub> _gameHub;

        public GameNotifcation(IHubContext<GameHub, IGameHub> gameHub)
        {
            _gameHub = gameHub;
        }


		public void Send(string title, string message)
        {
            _gameHub.Clients.All.Notification(title, message);
        }
    }
}