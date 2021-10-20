using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public abstract class BombCountPowerup : Powerup, IDataTransferable<BombCountPowerupDTO>
    {
        protected int _bombCountToAdd;

        public BombCountPowerup(Position position, int bombCountToAdd)
        {
            _position = position;
            _bombCountToAdd = bombCountToAdd;
        }

        public override void ApplyPowerup(Player player)
        {
            player.MaxBombCount += _bombCountToAdd;
        }

        public override void RemovePowerup(Player player)
        {
            player.MaxBombCount -= _bombCountToAdd;
        }

        public BombCountPowerupDTO ToDTO()
        {
            return new BombCountPowerupDTO()
            {
                Position = _position.ToDTO(),
                BombCountToAdd = _bombCountToAdd
            };
        }
    }
}
