using System;
using BoomermanServer.Game;

namespace BoomermanServer.Models
{
    public class Explosion : IComparable<Explosion>
    {
        private Position _position;
        private DateTime _timeLeft;
        private Player _owner;

        public Explosion(Position position, TimeSpan timeLeft, Player owner)
        {
            _position = position;
            _timeLeft = DateTime.Now.Add(timeLeft);
            _owner = owner;
        }

        public bool ShouldExplode()
        {
            return DateTime.Now >= _timeLeft;
        }

        public Position Position => _position;
        public Player Owner => _owner;

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
