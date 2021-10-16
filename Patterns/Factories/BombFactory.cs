using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Factories
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
                case BombType.Boomerang:
                    bomb = new BoomerangBomb(position);
                    break;
                default:
                    bomb = new RegularBomb(position);
                    break;
            }
            return bomb;
        }
    }
}
