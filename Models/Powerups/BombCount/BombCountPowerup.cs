using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Powerups.BombCount
{
    public abstract class BombCountPowerup : Powerup, IDataTransferable<BombCountPowerupDTO>
    {
        protected int _bombCountToAdd;
        protected Position _position;

        public BombCountPowerup(Position position, int bombCountToAdd)
        {
            _position = position;
            _bombCountToAdd = bombCountToAdd;
        }

        public override void ApplyPowerup(Player player)
        {
            player.MaxBombCount += _bombCountToAdd;
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