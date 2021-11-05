using System;
using BoomermanServer.Models;
using BoomermanServer.Models.Explosions;
using Newtonsoft.Json;

namespace BoomermanServer.Game
{
    public class MapManager
    {
        private string[][] originalMap =
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

        public string[][] map { get; private set; }

        public MapManager()
        {
            map = originalMap;
        }

        public string mapJSON()
        {
            return JsonConvert.SerializeObject(map, Formatting.Indented);
        }

        public Position CheckCollision(Position originalPos, Position newPos)
        {
            var newTLX = Convert.ToInt32(Math.Floor((newPos.X+2) / 32));
            var newTLY = Convert.ToInt32(Math.Floor((newPos.Y+2) / 32));
            var newBRX = Convert.ToInt32(Math.Floor((newPos.X+30) / 32));
            var newBRY = Convert.ToInt32(Math.Floor((newPos.Y+30) / 32));
            var orgTLX = Convert.ToInt32(Math.Floor((originalPos.X+2) / 32));
            var orgTLY = Convert.ToInt32(Math.Floor((originalPos.Y+2) / 32));
            var orgBRX = Convert.ToInt32(Math.Floor((originalPos.X+30) / 32));
            var orgBRY = Convert.ToInt32(Math.Floor((originalPos.Y+30) / 32));
            
            var posX = newPos.X;
            var posY = newPos.Y;
            var dX = originalPos.X - newPos.X;
            var dY = originalPos.Y - newPos.Y;
            switch (dX)
            {
                case > 0:
                {
                    if (map[orgBRY][newTLX] == "ndw" || map[orgTLY][newTLX] == "ndw")
                    {
                        posX = (newTLX + 1) * 32;
                    }

                    break;
                }
                case < 0:
                {
                    if (map[orgBRY][newBRX] == "ndw" || map[orgTLY][newBRX] == "ndw")
                    {
                        posX = (newBRX - 1) * 32;
                    }

                    break;
                }
            }

            switch (dY)
            {
                case > 0:
                {
                    if (map[newTLY][orgBRX] == "ndw" || map[newTLY][orgTLX] == "ndw")
                    {
                        posY = (newTLY + 1) * 32;
                    }

                    break;
                }
                case < 0:
                {
                    if (map[newBRY][orgBRX] == "ndw" || map[newBRY][orgTLX] == "ndw")
                    {
                        posY = (newBRY - 1) * 32;
                    }

                    break;
                }
            }
            
            return new Position(posX, posY);
        }

        public Position SnapBombPosition(Position position)
        {
            var posX = position.X;
            var posY = position.Y;
            double dX, dY;
            if ((dX = position.X % 32) > 16)
            {
                posX += 32 - dX;
            }
            else
            {
                posX -= dX;
            }
            if ((dY = position.Y % 32) > 16)
            {
                posY += 32 - dY;
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
                    var posX = Convert.ToInt32(Math.Floor((explosion.Position.X) / 32));
                    var posY = Convert.ToInt32(Math.Floor((explosion.Position.Y) / 32));

                    if (map[posY][posX] == "ndw")
                        break;
                    
                    newExplosions.AddExplosion(explosion);
                }
            }

            return newExplosions;
        }
    }
}