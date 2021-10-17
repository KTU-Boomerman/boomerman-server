using System.Collections.Generic;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Command
{
    public abstract class OnBeginAttributeSetter
    {
        protected List<Player> _players;

        public OnBeginAttributeSetter(List<Player> players)
        {
            _players = players;
        }

        public abstract void SetAttributes();
        public abstract void Undo();
    }
}
