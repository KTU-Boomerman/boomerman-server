using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using BoomermanServer.Data;

namespace BoomermanServer.Hubs
{
    public class GameHub : Hub
    {
        public async Task UpdatePlayer(UpdatePlayerDTO player)
        {
            await Clients.Others.SendAsync("UpdateEnemy", player);
        }
    }
}