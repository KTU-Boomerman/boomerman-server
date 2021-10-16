using boomerman_server.Models.Walls;

namespace boomerman_server.Patterns
{
    public class WallBuilder
    {
        private readonly Wall _wall = new();
        public void SetWallType(bool isDestructible)
        {
            _wall.IsDestructible = isDestructible;
        }

        public Wall GetWall() => _wall;
    }
}