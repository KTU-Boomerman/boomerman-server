using System;
using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models
{
    public class WaveBomb : Bomb
    {
        public WaveBomb(Position position)
            : base(position)
        {
            _bombType = BombType.Wave;
        }

        public override void Explode()
        {
            Console.WriteLine("Wave explosion");
        }
    }
}
