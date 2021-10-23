using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups
{
    public abstract class Powerup
    {
        protected Position _position;

        public abstract void ApplyPowerup(Player player);

        public abstract void RemovePowerup(Player player);
    }
}
