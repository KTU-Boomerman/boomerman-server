using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Patterns.Builders
{
    public abstract class BombBuilder
    {
        protected Bomb Bomb;
        public abstract BombBuilder SetPosition(Position position);
        public abstract Bomb GetBomb();
    }
}