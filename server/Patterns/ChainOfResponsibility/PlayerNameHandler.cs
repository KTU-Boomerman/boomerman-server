using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
	public class PlayerNameHandler : AbstractChatHandler
	{
        private IPlayerManager _playerManager;
		public PlayerNameHandler(IPlayerManager playerManager) : base() 
        {
            _playerManager = playerManager;
        }
		public override Message Handle(Message message)
		{   
            var playerNames = _playerManager.GetPlayers().Select(p => p.Name);

            if (playerNames.Any(message.Text.Contains))
            {
                message.Text = playerNames.Aggregate(message.Text,
                    (text, playerName) => text.Replace(playerName, $"<span class=\"player\">{playerName}</span>"));
                
                return base.Handle(message);
            } 

			return base.Handle(message);
		}
	}
}