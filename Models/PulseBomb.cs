using System;
using BoomermanServer.Game;

namespace BoomermanServer.Models
{
    public class PulseBomb : Bomb
    {
        public PulseBomb(Position position)
            : base(position) { }

        public override void Explode()
        {
            Console.WriteLine("Pulse explosion");
        }
    }
}
