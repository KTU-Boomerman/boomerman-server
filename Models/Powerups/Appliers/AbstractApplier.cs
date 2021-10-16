using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Appliers
{
    public abstract class AbstractApplier
    {
        protected Powerup _powerup { get; set; }

        public abstract void ApplyPowerup(Player player);
    }
}
