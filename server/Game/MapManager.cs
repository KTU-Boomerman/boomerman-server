using System;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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
            var newTLX = Convert.ToInt32(Math.Floor((newPos.X+2) / _cellSize));
            var newTLY = Convert.ToInt32(Math.Floor((newPos.Y+2) / _cellSize));
            var newBRX = Convert.ToInt32(Math.Floor((newPos.X+30) / _cellSize));
            var newBRY = Convert.ToInt32(Math.Floor((newPos.Y+30) / _cellSize));
            var orgTLX = Convert.ToInt32(Math.Floor((originalPos.X+2) / _cellSize));
            var orgTLY = Convert.ToInt32(Math.Floor((originalPos.Y+2) / _cellSize));
            var orgBRX = Convert.ToInt32(Math.Floor((originalPos.X+30) / _cellSize));
            var orgBRY = Convert.ToInt32(Math.Floor((originalPos.Y+30) / _cellSize));
            
            var posX = newPos.X;
            var posY = newPos.Y;
            var dX = originalPos.X - newPos.X;
            var dY = originalPos.Y - newPos.Y;
            switch (dX)
            {
                case > 0:
                    if (collidableTiles.Contains(map[orgBRY][newTLX]) || collidableTiles.Contains(map[orgTLY][newTLX]))
                        posX = (newTLX + 1) * _cellSize;
                    break;
                case < 0:
                    if (collidableTiles.Contains(map[orgBRY][newBRX]) || collidableTiles.Contains(map[orgTLY][newBRX]))
                        posX = (newBRX - 1) * _cellSize;
                    break;
            }

            switch (dY)
            {
                case > 0:
                    if (collidableTiles.Contains(map[newTLY][orgBRX]) || collidableTiles.Contains(map[newTLY][orgTLX]))
                        posY = (newTLY + 1) * _cellSize;
                    break;
                case < 0:
                    if (collidableTiles.Contains(map[newBRY][orgBRX]) || collidableTiles.Contains(map[newBRY][orgTLX]))
                        posY = (newBRY - 1) * _cellSize;
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
            if ((dX = position.X % _cellSize) > 16)
            {
                posX += _cellSize - dX;
            }
            else
            {
                posX -= dX;
            }
            if ((dY = position.Y % _cellSize) > 16)
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
            var orgTLX = Convert.ToInt32(Math.Floor((position.X+2) / _cellSize));
            var orgTLY = Convert.ToInt32(Math.Floor((position.Y+2) / _cellSize));
            var orgBRX = Convert.ToInt32(Math.Floor((position.X+30) / _cellSize));
            var orgBRY = Convert.ToInt32(Math.Floor((position.Y+30) / _cellSize));
            
            for (int i = orgTLY; i <= orgBRY; i++)
                for (int j = orgTLX; j <= orgBRX; j++)
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
    }
}