using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Explosions
{
    public class Explosions
    {
        public Explosion Origin { get; set; }

        public Dictionary<Direction, LinkedList<Explosion>> ExplosionsByDirection { get; set; }

        public Explosions(Explosion origin, List<Explosion> explosions)
        {
            Origin = origin;
            ExplosionsByDirection = new Dictionary<Direction, LinkedList<Explosion>>();

            CreateExplosionDictionary();
            AddExplosions(explosions);
        }
        
        public Explosions(Explosion origin)
        {
            Origin = origin;
            ExplosionsByDirection = new Dictionary<Direction, LinkedList<Explosion>>();

            CreateExplosionDictionary();
        }

        public void CreateExplosionDictionary()
        {
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                ExplosionsByDirection.Add(direction, new LinkedList<Explosion>());
        }

        public void ClearExplosions() 
        {
            Origin = null;
            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                ExplosionsByDirection[direction].Clear();
        }

        public void ClearExplosionsByDirection(Direction direction)
        {
            ExplosionsByDirection[direction].Clear();
        }

        public void AddExplosions(List<Explosion> explosions)
        {
            foreach (var explosion in explosions)
                AddExplosion(explosion);
        }

        public void AddExplosion(Explosion explosion)
        {
            var direction = Origin.Position.GetDirection(explosion.Position);

            ExplosionsByDirection[direction].AddLast(explosion);
        }

        public void RemoveExplosionsByDirection(Direction direction)
        {
            ExplosionsByDirection[direction].Clear();
        }

        public void RemoveExplosionsByPosition(Position position)
        {
            if (position == Origin.Position)
            {
                ClearExplosions();
                return;
            }

            var direction = Origin.Position.GetDirection(position);
            ClearExplosionsByDirection(direction);
        }

        public void RemoveExplosionsByExplosion(Explosion explosion)
        {
            RemoveExplosionsByPosition(explosion.Position);
        }

        public LinkedList<Explosion> GetExplosionsByDirection(Direction direction)
        {
            return ExplosionsByDirection[direction];
        }

        public List<Explosion> ToList()
        {
            var explosions = new List<Explosion> { Origin };

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
                explosions.AddRange(ExplosionsByDirection[direction]);

            return explosions;
        }
    }
}