using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Powerups.Health
{
    public abstract class HealthPowerup : Powerup, IDataTransferable<HealthPowerupDTO>
    {
        protected int _healthAmount;
        protected Position _position;

        public HealthPowerup(Position position, int healthAmount)
        {
            _position = position;
            _healthAmount = healthAmount;
        }

        public override void ApplyPowerup(Player player)
        {
            player.Health += _healthAmount;
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
