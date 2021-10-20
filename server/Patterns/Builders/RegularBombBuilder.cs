using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Builders
{
    public class RegularBombBuilder : BombBuilder
    {
        public RegularBombBuilder()
        {
            Bomb = new RegularBomb();
        }

        public override BombBuilder SetPosition(Position position)
        {
            Bomb.SetPosition(position);
            return this;
        }

        public override Bomb GetBomb() => Bomb;
    }
}
