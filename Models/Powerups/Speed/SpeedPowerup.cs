using BoomermanServer.Game;
using BoomermanServer.Data;

namespace BoomermanServer.Models.Powerups.Speed
{
    public abstract class SpeedPowerup : IDataTransferable<SpeedPowerupDTO>
    {
        protected double _speedPercentage;
        protected Position _position;

        public abstract SpeedPowerup Clone();

        public SpeedPowerup(Position position, double speedPercentage)
        {
            _position = position;
            _speedPercentage = speedPercentage;
        }

        public SpeedPowerupDTO ToDTO()
        {
            return new SpeedPowerupDTO()
            {
                Position = _position.ToDTO(),
                SpeedPercentage = _speedPercentage
            };
        }
    }
}
