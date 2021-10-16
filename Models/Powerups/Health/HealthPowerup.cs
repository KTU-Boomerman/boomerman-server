using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Powerups.Health
{
    public abstract class HealthPowerup : IDataTransferable<HealthPowerupDTO>
    {
        protected int _healthAmount;
        protected Position _position;

        public HealthPowerup(Position position, int healthAmount)
        {
            _position = position;
            _healthAmount = healthAmount;
        }

        public HealthPowerupDTO ToDTO()
        {
            return new HealthPowerupDTO()
            {
                Position = _position.ToDTO(),
                HealthAmount = _healthAmount
            };
        }
    }
}
