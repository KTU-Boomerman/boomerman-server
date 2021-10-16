using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Appliers
{
    public abstract class AbstractApplier
    {
        public Powerup Powerup { protected get; set; }

        public abstract void ApplyPowerup(Player player);
    }
}
