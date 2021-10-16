namespace BoomermanServer.Models.Powerups.Health
{
    public abstract class HealthPowerup
    {
        protected int _healthAmount;

        public HealthPowerup(int healthAmount)
        {
            _healthAmount = healthAmount;
        }
    }
}
