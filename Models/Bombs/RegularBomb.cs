using System;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class RegularBomb : Bomb
    {
        public RegularBomb()
        {
            _bombType = BombType.Regular;
        }

        public override void Explode()
        {
            Console.WriteLine("Regular explosion");
        }

        public override void SetPosition(Position position)
        {
            _position = position;
        }
    }
}
