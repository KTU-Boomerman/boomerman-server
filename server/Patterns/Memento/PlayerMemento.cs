using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Memento
{
	public class PlayerMemento : IMemento
	{
        private Player _player;
        private PlayerDTO _state;

        public PlayerMemento(Player player, PlayerDTO state)
        {
            _player = player;
            _state = state;
        }

		public void Restore()
		{
			_player.Restore(_state);
		}
	}
}