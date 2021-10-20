using System;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Bombs
{
    public class BoomerangBomb : Bomb
    {
        public BoomerangBomb()
        {
            _bombType = BombType.Boomerang;
        }
        public override void SetPosition(Position position)
        {
            _position = position;
        }
        public override void Explode()
        {
            Console.WriteLine("Boomerang explosion"); // TODO: Add actual explosion logic
        }
    }
}
