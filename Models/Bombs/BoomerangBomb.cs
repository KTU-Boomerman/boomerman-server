using System;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class BoomerangBomb : Bomb
    {
        public BoomerangBomb(Position position)
            : base(position)
        {
            _bombType = BombType.Boomerang;
        }

        public override void Explode()
        {
            Console.WriteLine("Boomerang explosion"); // TODO: Add actual explosion logic
        }
    }
}
