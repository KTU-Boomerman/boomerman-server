using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public class SmallBombCountPowerup : BombCountPowerup
    {
        public SmallBombCountPowerup(Position position)
            : base(position, 1) { }
    }
}
