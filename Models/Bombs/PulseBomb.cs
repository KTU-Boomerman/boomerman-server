using System;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class PulseBomb : Bomb
    {
        public PulseBomb()
        {
            _bombType = BombType.Pulse;
        }
        public override void SetPosition(Position position)
        {
            _position = position;
        }

        public override void Explode()
        {
            Console.WriteLine("Pulse explosion");
        }


    }
}
