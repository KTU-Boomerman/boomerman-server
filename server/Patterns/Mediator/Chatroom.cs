using System.Collections.Generic;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Mediator
{
    public class Chatroom : IChatroom
    {
        private Dictionary<string, Player> participants = new();

        public void Register(Player player)
        {
            if(!participants.ContainsValue(player))
            {
                participants[player.ID] = player;
            }
            player.Chatroom = this;
        }

        public void Send(string from, string to, string message)
        {
            var participant = participants[to];
            if(participant != null)
            {
                // TODO: Add direct message logic here
            }
        }
    }
}
