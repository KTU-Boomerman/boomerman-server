using System;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;
using System.Collections.Generic;
using System.Linq;
using BoomermanServer.Models.Powerups;

namespace BoomermanServer.Game
{
    public class MapManager
    {
        private const int _cellSize = 32;

        private readonly string[][] _originalMap =
        {
            new []{"ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw"},
            new []{"ndw","grs","grs","grs","dew","dew","dew","dew","grs","dew","dew","dew","dew","grs","grs","grs","ndw"},
            new []{"ndw","grs","ndw","ndw","dew","grs","grs","dew","ndw","dew","grs","grs","dew","ndw","ndw","grs","ndw"},
            new []{"ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw"},
            new []{"ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw"},
            new []{"ndw","grs","dew","dew","dew","ndw","ndw","dew","grs","dew","ndw","ndw","dew","dew","dew","grs","ndw"},
            new []{"ndw","grs","ndw","ndw","dew","dew","dew","dew","grs","dew","dew","dew","dew","ndw","ndw","grs","ndw"},
            new []{"ndw","grs","dew","dew","dew","ndw","ndw","dew","grs","dew","ndw","ndw","dew","dew","dew","grs","ndw"},
            new []{"ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw"},
            new []{"ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw"},
            new []{"ndw","grs","ndw","ndw","dew","grs","grs","dew","ndw","dew","grs","grs","dew","ndw","ndw","grs","ndw"},
            new []{"ndw","grs","grs","grs","dew","dew","dew","dew","grs","dew","dew","dew","dew","grs","grs","grs","ndw"},
            new []{"ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw"},
        };

        private string[] collidableTiles = {"ndw", "dew"};

        public string[][] map { get; private set; }

        public MapManager()
        {
            map = _originalMap;
        }

        public Position CheckCollision(Position originalPos, Position newPos)
        {
            var newLeftX = Convert.ToInt32(Math.Floor((newPos.X+2) / _cellSize));
            var newTopY = Convert.ToInt32(Math.Floor((newPos.Y+2) / _cellSize));
            var newRightX = Convert.ToInt32(Math.Floor((newPos.X+30) / _cellSize));
            var newBottomY = Convert.ToInt32(Math.Floor((newPos.Y+30) / _cellSize));
            var orgLeftX = Convert.ToInt32(Math.Floor((originalPos.X+2) / _cellSize));
            var orgTopY = Convert.ToInt32(Math.Floor((originalPos.Y+2) / _cellSize));
            var orgRightX = Convert.ToInt32(Math.Floor((originalPos.X+30) / _cellSize));
            var orgBottomY = Convert.ToInt32(Math.Floor((originalPos.Y+30) / _cellSize));
            
            var posX = newPos.X;
            var posY = newPos.Y;
            var dX = originalPos.X - newPos.X;
            var dY = originalPos.Y - newPos.Y;
            switch (dX)
            {
                case > 0:
                    // left side
                    if (collidableTiles.Contains(map[orgBottomY][newLeftX]) || collidableTiles.Contains(map[orgTopY][newLeftX]))
                        posX = (newLeftX + 1) * _cellSize - 1;
                    break;
                case < 0:
                    // right side
                    if (collidableTiles.Contains(map[orgBottomY][newRightX]) || collidableTiles.Contains(map[orgTopY][newRightX]))
                        posX = (newRightX - 1) * _cellSize + 1;
                    break;
            }

            switch (dY)
            {
                case > 0:
                    // top side
                    if (collidableTiles.Contains(map[newTopY][orgRightX]) || collidableTiles.Contains(map[newTopY][orgLeftX]))
                        posY = (newTopY + 1) * _cellSize - 1;
                    break;
                case < 0:
                    // bottom side
                    if (collidableTiles.Contains(map[newBottomY][orgRightX]) || collidableTiles.Contains(map[newBottomY][orgLeftX]))
                        posY = (newBottomY - 1) * _cellSize + 1;
                    break;
            }

            return new Position(posX, posY);
        }

        public List<Position> GetDestructibleWalls()
        {
            var results = new List<Position>();
            for (int i = 0; i < map.Length; i++)
            {
                var line = map[i];
                for (int j = 0; j < line.Length; j++)
                {
                    if (map[i][j] == "dew")
                        results.Add(new Position(j*_cellSize, i*_cellSize));
                }
            }
            return results;
        }

        public Position SnapBombPosition(Position position)
        {
            var posX = position.X;
            var posY = position.Y;
            double dX, dY;
            if ((dX = position.X % _cellSize) > _cellSize / 2.0)
            {
                posX += _cellSize - dX;
            }
            else
            {
                posX -= dX;
            }
            if ((dY = position.Y % _cellSize) > _cellSize / 2.0)
            {
                posY += _cellSize - dY;
            }
            else
            {
                posY -= dY;
            }
            return new Position(posX, posY);
        }

        public Explosions FilterExplosions(Explosions explosions)
        {
            var newExplosions = new Explosions(explosions.Origin);

            foreach (Direction direction in Enum.GetValues(typeof(Direction)))
            {
                foreach (var explosion in explosions.GetExplosionsByDirection(direction))
                {
                    var pos = GetMapPos(explosion.Position);

                    if (map[pos.Item1][pos.Item2] == "ndw")
                        break;
                    
                    newExplosions.AddExplosion(explosion);
                    
                    if (map[pos.Item1][pos.Item2] == "dew")
                        break;
                }
            }

            return newExplosions;
        }

        public void SetExplosion(Position position)
        {
            var pos = GetMapPos(position);
            map[pos.Item1][pos.Item2] = "exp";
        }

        public void SetGrass(Position position)
        {
            var pos = GetMapPos(position);
            map[pos.Item1][pos.Item2] = "grs";
        }

        public bool IsInExplosion(Position position)
        {
            var leftX = Convert.ToInt32(Math.Floor((position.X+2) / _cellSize));
            var topY = Convert.ToInt32(Math.Floor((position.Y+2) / _cellSize));
            var rightX = Convert.ToInt32(Math.Floor((position.X+30) / _cellSize));
            var bottomY = Convert.ToInt32(Math.Floor((position.Y+30) / _cellSize));
            
            for (int i = topY; i <= bottomY; i++)
                for (int j = leftX; j <= rightX; j++)
                    if (map[i][j] == "exp")
                        return true;

            return false;
        }

        /// <summary>
        /// Returns id's to interact with map. Returns Item1 - Y; Item2 - X
        /// </summary>
        /// <param name="pos"></param>
        private (int, int) GetMapPos(Position pos)
        {
            return (Convert.ToInt32(Math.Floor(pos.Y / _cellSize)), Convert.ToInt32(Math.Floor(pos.X / _cellSize)));
        }

        public bool CanPowerupSpawn(Position position)
        {
            var pos = GetMapPos(position);
            return map[pos.Item1][pos.Item2] == "dew";
        }

        public void SetPowerup(Powerup powerup)
        {
            var pos = GetMapPos(powerup.GetPosition());
            map[pos.Item1][pos.Item2] = "pwu";
        }
    }
}