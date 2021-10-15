namespace BoomermanServer.Models
{
    public class Explosion
    {
        private Bomb _bomb;

        public Explosion(Bomb bomb)
        {
            _bomb = bomb;
        }

        public void Explode()
        {
            _bomb.Explode();
        }
    }
}