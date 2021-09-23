using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BoomermanServer.Hubs
{
    public class GameHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdatePlayer(Player player)
        {
            await Clients.Others.SendAsync("UpdateEnemy", player);
        }
    }
}