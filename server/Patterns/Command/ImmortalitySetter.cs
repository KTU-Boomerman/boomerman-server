using System.Collections.Generic;
using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Command
{
    public class ImmortalitySetter : OnBeginAttributeSetter
    {
        public ImmortalitySetter(List<Player> players)
            : base(players) { }

        public override void SetAttributes()
        {
            foreach (var player in _players)
            {
                player.IsImmortal = true;
            }
        }

        public override void Undo()
        {
            foreach (var player in _players)
            {
                player.IsImmortal = false;
            }
        }
    }
}
