using System;

namespace BoomermanServer.Game
{
    public class MapManager
    {
        private string[,] map =
        {
            {"ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw"},
            {"ndw","grs","grs","grs","dew","dew","dew","dew","grs","dew","dew","dew","dew","grs","grs","grs","ndw"},
            {"ndw","grs","ndw","ndw","dew","grs","grs","dew","ndw","dew","grs","grs","dew","ndw","ndw","grs","ndw"},
            {"ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw"},
            {"ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw"},
            {"ndw","grs","dew","dew","dew","ndw","ndw","dew","grs","dew","ndw","ndw","dew","dew","dew","grs","ndw"},
            {"ndw","grs","ndw","ndw","dew","dew","dew","dew","grs","dew","dew","dew","dew","ndw","ndw","grs","ndw"},
            {"ndw","grs","dew","dew","dew","ndw","ndw","dew","grs","dew","ndw","ndw","dew","dew","dew","grs","ndw"},
            {"ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw"},
            {"ndw","dew","dew","dew","dew","ndw","ndw","dew","ndw","dew","ndw","ndw","dew","dew","dew","dew","ndw"},
            {"ndw","grs","ndw","ndw","dew","grs","grs","dew","ndw","dew","grs","grs","dew","ndw","ndw","grs","ndw"},
            {"ndw","grs","grs","grs","dew","dew","dew","dew","grs","dew","dew","dew","dew","grs","grs","grs","ndw"},
            {"ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw","ndw"},
        };

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
                    if (map[orgBRY, newTLX] == "ndw" || map[orgTLY, newTLX] == "ndw")
                    {
                        posX = (newTLX + 1) * 32;
                    }

                    break;
                }
                case < 0:
                {
                    if (map[orgBRY, newBRX] == "ndw" || map[orgTLY, newBRX] == "ndw")
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
                    if (map[newTLY, orgBRX] == "ndw" || map[newTLY, orgTLX] == "ndw")
                    {
                        posY = (newTLY + 1) * 32;
                    }

                    break;
                }
                case < 0:
                {
                    if (map[newBRY, orgBRX] == "ndw" || map[newBRY, orgTLX] == "ndw")
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
            Console.WriteLine($"{position.X} {position.Y} {posX} {posY}");
            return new Position(posX, posY);
        }
    }
}