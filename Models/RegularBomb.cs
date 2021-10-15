using System;
using BoomermanServer.Game;

namespace BoomermanServer.Models
{
    public class RegularBomb : Bomb
    {
        public RegularBomb(Position position)
            : base(position) { }

        public override void Explode()
        {
            Console.WriteLine("Regular explosion");
        }
    }
}
