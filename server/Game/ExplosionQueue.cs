using System;
using System.Collections.Generic;
using BoomermanServer.Models;

namespace BoomermanServer.Game
{
    public class ExplosionQueue : SortedSet<Explosion>, IExplosionQueue {}
}