using System;
using BoomermanServer.Data;

namespace BoomermanServer.Game
{
    [Serializable]
    public class Position : IDataTransferable<PositionDTO>
    {
        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Position(PositionDTO dto)
        {
            X = dto.X;
            Y = dto.Y;
        }

        public double X { get; set; }
        public double Y { get; set; }

        public PositionDTO ToDTO()
        {
            return new PositionDTO()
            {
                X = X,
                Y = Y
            };
        }

        public override bool Equals(object obj)
        {
            if(obj is Position)
            {
                var position = obj as Position;
                return position.X == X && position.Y == Y;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
