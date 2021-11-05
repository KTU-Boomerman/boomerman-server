using BoomermanServer.Hubs;

/**
 * Here's an example of the existing class that follows the Target interface.
 */
namespace BoomermanServer.Patterns.Adapter
{
    public class GameNotifcation : Notification
    {
        private GameHub _gameHub;

        public GameNotifcation(GameHub gameHub)
        {
            _gameHub = gameHub;
        }


		public void Send(string title, string message)
        {
            _gameHub.Clients.All.Notification(title, message);
        }
    }
}