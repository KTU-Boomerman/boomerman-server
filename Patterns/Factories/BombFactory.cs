using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;
using BoomermanServer.Patterns.Builders;

namespace BoomermanServer.Patterns.Factories
{
    public class BombFactory
    {
        private BombBuilder _bombBuilder;
        public Bomb CreateBomb(BombType bombType, Position position)
        {
            Bomb bomb = null;
            switch (bombType)
            {
                case BombType.Wave:
                    _bombBuilder = new WaveBombBuilder();
                    _bombBuilder.SetPosition(position).GetBomb();
                    break;
                case BombType.Pulse:
                    _bombBuilder = new PulseBombBuilder();
                    _bombBuilder.SetPosition(position).GetBomb();
                    break;
                default:
                    _bombBuilder = new RegularBombBuilder();
                    _bombBuilder.SetPosition(position).GetBomb();
                    break;
            }
            return bomb;
        }
    }
}
