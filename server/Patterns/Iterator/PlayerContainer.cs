using System.Collections.Generic;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Iterator
{
    public class PlayerContainer
    {
        private Player[] players;

        public PlayerContainer(List<Player> players)
        {
            this.players = players.ToArray();
        }

        public IIterator GetIterator()
        {
            return new PlayerIterator(this);
        }

        public Player this[int index]
        {
            get { return players[index]; }
            set { players[index] = value; }
        }

        private class PlayerIterator : IIterator
        {
            private PlayerContainer playerContainer;
            private int current;

            public PlayerIterator(PlayerContainer playerContainer)
            {
                this.playerContainer = playerContainer;
                current = 0;
            }

            public object CurrentItem()
            {
                return playerContainer[current];
            }

            public object First()
            {
                return playerContainer[0];
            }

            public bool IsDone()
            {
                return current >= playerContainer.players.Length;
            }

            public object Next()
            {
                return playerContainer[++current];
            }
        }
    }
}