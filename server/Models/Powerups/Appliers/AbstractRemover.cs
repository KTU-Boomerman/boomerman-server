using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Appliers
{
    public abstract class AbstractRemover
    {
        public Powerup Powerup { protected get; set; }

        public abstract void RemovePowerup(Player player);
    }
}
