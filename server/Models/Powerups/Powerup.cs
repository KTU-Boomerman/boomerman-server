using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups
{
    public abstract class Powerup
    {
        public abstract void ApplyPowerup(Player player);

        public abstract void RemovePowerup(Player player);

        public abstract Powerup Clone();

        public abstract Powerup DeepClone();
    }
}
