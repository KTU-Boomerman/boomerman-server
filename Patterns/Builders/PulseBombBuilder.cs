using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Builders
{
    public class PulseBombBuilder : BombBuilder
    {
        public PulseBombBuilder()
        {
            Bomb = new PulseBomb();
        }

        public override PulseBombBuilder SetPosition(Position position)
        {
            Bomb.SetPosition(position);
            return this;
        }

        public override Bomb GetBomb() => Bomb;

    }
}