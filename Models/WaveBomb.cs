using System;

namespace BoomermanServer.Models
{
    public class WaveBomb : Bomb
    {
        public override void Explode()
        {
            Console.WriteLine("Wave explosion");
        }
    }
}
