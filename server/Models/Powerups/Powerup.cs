using BoomermanServer.Data;
using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups
{
    public abstract class Powerup
    {
        protected Position _position;

        protected PowerupType _powerupType;

        public abstract void ApplyPowerup(Player player);

        public abstract void RemovePowerup(Player player);

        public Position GetPosition()
        {
            return _position;
        }

        public PowerupDTO ToDTO()
        {
            return new PowerupDTO()
            {
                Position = _position.ToDTO(),
                PowerupType = _powerupType
            };
        }
    }
}
