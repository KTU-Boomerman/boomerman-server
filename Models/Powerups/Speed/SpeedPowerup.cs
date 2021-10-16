namespace BoomermanServer.Models.Powerups.Speed
{
    public abstract class SpeedPowerup
    {
        protected double _increasePercentage;

        public SpeedPowerup(double increasePercentage)
        {
            _increasePercentage = increasePercentage;
        }
    }
}
