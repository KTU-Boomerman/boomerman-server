using System;
using BoomermanServer.Game;

namespace BoomermanServer.Models
{
    public class WaveBomb : Bomb
    {
        public WaveBomb(Position position)
            : base(position) { }

        public override void Explode()
        {
            Console.WriteLine("Wave explosion");
        }
    }
}
