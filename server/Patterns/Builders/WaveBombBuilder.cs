using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Builders
{
    public class WaveBombBuilder : BombBuilder
    {
        public WaveBombBuilder()
        {
            Bomb = new WaveBomb();
        }

        public override BombBuilder SetPosition(Position position)
        {
            Bomb.SetPosition(position);
            return this;
        }

        public override Bomb GetBomb() => Bomb;
    }
}
