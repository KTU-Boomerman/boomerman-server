using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public class BigBombCountPowerup : BombCountPowerup
    {
        public BigBombCountPowerup(Position position)
            : base(position, 2) { }
    }
}
