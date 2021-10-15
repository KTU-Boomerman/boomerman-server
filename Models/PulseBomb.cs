using System;

namespace BoomermanServer.Models
{
    public class PulseBomb : Bomb
    {
        public override void Explode()
        {
            Console.WriteLine("Pulse explosion");
        }
    }
}
