using System;

namespace BoomermanServer.Models
{
    public class RegularBomb : Bomb
    {
        public override void Explode()
        {
            Console.WriteLine("Regular explosion");
        }
    }
}
