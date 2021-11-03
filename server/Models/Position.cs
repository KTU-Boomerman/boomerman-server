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

        public static Position operator +(Position position1, Position position2)
        {
            return new Position(position1.X + position2.X, position1.Y + position2.Y);
        }

        public static Position operator -(Position position1, Position position2)
        {
            return new Position(position1.X - position2.X, position1.Y - position2.Y);
        }

        public static Position operator *(Position position, double scalar)
        {
            return new Position(position.X * scalar, position.Y * scalar);
        }
        
        public static Position operator /(Position position, double scalar)
        {
            return new Position(position.X / scalar, position.Y / scalar);
        }

        public static bool operator ==(Position position1, Position position2)
        {
            return position1.X == position2.X && position1.Y == position2.Y;
        }

        public static bool operator !=(Position position1, Position position2)
        {
            return position1.X != position2.X || position1.Y != position2.Y;
        }

        public override string ToString()
        {
            return $"({X}, {Y})";
        }

        public static Position Zero
        {
            get
            {
                return new Position(0, 0);
            }
        }
    }
}
