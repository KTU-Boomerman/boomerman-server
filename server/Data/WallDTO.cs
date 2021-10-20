using BoomermanServer.Data;

namespace boomerman_server.Data
{
    public class WallDTO
    {
        public PositionDTO Position { get; set; }
        public bool IsDestructible { get; set; }
    }
}