using System;
using BoomermanServer.Game;

namespace BoomermanServer.Models
{
    public class Explosion : IComparable<Explosion>
    {
        private Position _position;
        private DateTime _timeLeft;
        private DateTime _spawnTime;

        public Explosion(Position position, TimeSpan timeLeft)
        {
            _position = position;
            _timeLeft = DateTime.Now.Add(timeLeft);
            _spawnTime = DateTime.Now;
        }

        public bool ShouldExplode()
        {
            return DateTime.Now >= _timeLeft;
        }

        public Position Position => _position;

        public int Compare(Explosion x, Explosion y)
        {
            return x._timeLeft.CompareTo(y._timeLeft);
        }

		public int CompareTo(Explosion other)
		{
            return Compare(this, other);
		}
	}
}
