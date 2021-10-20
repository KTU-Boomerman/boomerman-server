using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Builders
{
    public class BoomerangBombBuilder : BombBuilder
    {
        public BoomerangBombBuilder()
        {
            Bomb = new BoomerangBomb();
        }

        public override BoomerangBombBuilder SetPosition(Position position)
        {
            Bomb.SetPosition(position);
            return this;
        }

        public override Bomb GetBomb() => Bomb;

    }
}
