using System.Collections.Generic;
using BoomermanServer.Game;
using BoomermanServer.Patterns.Iterator;

namespace BoomermanServer.Patterns.Command
{
    public abstract class OnBeginAttributeSetter
    {
        protected PlayerContainer _players;

        public OnBeginAttributeSetter(PlayerContainer players)
        {
            _players = players;
        }

        public abstract void SetAttributes();
        public abstract void Undo();
    }
}
