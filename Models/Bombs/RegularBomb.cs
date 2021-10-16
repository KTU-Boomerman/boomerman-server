using System;
using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Bombs
{
    public class RegularBomb : Bomb
    {
        public RegularBomb(Position position)
            : base(position)
        {
            _bombType = BombType.Regular;
        }

        public override void Explode()
        {
            Console.WriteLine("Regular explosion"); // TODO: Add actual explosion logic
        }
    }
}
