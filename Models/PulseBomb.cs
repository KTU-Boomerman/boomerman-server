using System;
using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models
{
    public class PulseBomb : Bomb
    {
        public PulseBomb(Position position)
            : base(position)
        {
            _bombType = BombType.Pulse;
        }

        public override void Explode()
        {
            Console.WriteLine("Pulse explosion");
        }
    }
}
