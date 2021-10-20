using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Appliers
{
    public class ConcreteRemover : AbstractRemover
    {
        public override void RemovePowerup(Player player)
        {
            Powerup.RemovePowerup(player);
        }
    }
}
