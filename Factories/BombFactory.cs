using BoomermanServer.Models.Bombs;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Factories
{
    public class BombFactory
    {
        public Bomb CreateBomb(BombType bombType, Position position)
        {
            Bomb bomb = null;
            switch (bombType)
            {
                case BombType.Wave:
                    bomb = new WaveBomb(position);
                    break;
                case BombType.Pulse:
                    bomb = new PulseBomb(position);
                    break;
                default:
                    bomb = new RegularBomb(position);
                    break;
            }
            return bomb;
        }
    }
}
