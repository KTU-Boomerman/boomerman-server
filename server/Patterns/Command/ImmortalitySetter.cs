using BoomermanServer.Game;
using BoomermanServer.Patterns.Iterator;

namespace BoomermanServer.Patterns.Command
{
    public class ImmortalitySetter : OnBeginAttributeSetter
    {
        public ImmortalitySetter(PlayerContainer players)
            : base(players) { }

        public override void SetAttributes()
        {
            var iterator = _players.GetIterator();
            while (!iterator.IsDone())
            {
                var player = iterator.CurrentItem() as Player;
                player.IsImmortal = true;
                iterator.Next();
            }
        }

        public override void Undo()
        {
            var iterator = _players.GetIterator();
            while (!iterator.IsDone())
            {
                var player = iterator.CurrentItem() as Player;
                player.IsImmortal = false;
                iterator.Next();
            }
        }
    }
}
