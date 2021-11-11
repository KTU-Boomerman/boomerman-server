using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Data;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Game
{
    public class BombManager
    {
        private List<Bomb> _bombs;

        public BombManager()
        {
            _bombs = new();
        }

        public void AddBomb(Bomb bomb)
        {
            _bombs.Add(bomb);
        }

        public int GetPlayerBombCount(Player player)
        {
            return _bombs.Aggregate(0, (cnt, b) =>
            {
                if (b.Owner.ID == player.ID)
                {
                    return cnt + b.BombWeight;
                }
                
                return cnt;
            });
            // return _bombs.Count(b => b.Owner.ID == player.ID);
        }

        public void RemoveBomb(Player player, Position position)
        {
            _bombs.RemoveAll(b => b.Position == position && b.Owner.ID == player.ID);
        }
    }
}