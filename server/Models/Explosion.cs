using System.Collections.Generic;
using System.Timers;
using BoomermanServer.Models.Bombs;

namespace BoomermanServer.Models
{
    public class Explosion
    {
        private Bomb _bomb;
        private Queue<Explosion> _pendingExplosions;
        private Timer _timer;
        private const int ExplosionInterval = 3000;

        public Explosion(Bomb bomb, Queue<Explosion> pendingExplosions)
        {
            _bomb = bomb;
            _pendingExplosions = pendingExplosions;
            SetupTimer();
        }

        public void Explode(object sender, ElapsedEventArgs args)
        {
            if (_pendingExplosions.Count > 0)
            {
                _timer.Stop();
                _bomb.Explode();
                _pendingExplosions.Dequeue();
            }
        }

        private void SetupTimer()
        {
            _timer = new Timer(ExplosionInterval);
            _timer.Elapsed += new ElapsedEventHandler(Explode);
            _timer.Start();
        }
    }
}
