using System;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class WaveBomb : Bomb
    {
        public WaveBomb()
        {
            _bombType = BombType.Wave;
        }

        public override void Explode()
        {
            Console.WriteLine("Wave explosion");
        }

        public override void SetPosition(Position position)
        {
            _position = position;
        }
    }
}
