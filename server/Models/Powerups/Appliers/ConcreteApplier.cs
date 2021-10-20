using BoomermanServer.Game;

namespace BoomermanServer.Models.Powerups.Appliers
{
    public class ConcreteApplier : AbstractApplier
    {
        public override void ApplyPowerup(Player player)
        {
            Powerup.ApplyPowerup(player);
        }
    }
}
