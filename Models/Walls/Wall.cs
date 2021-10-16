using boomerman_server.Data;
using BoomermanServer.Data;
using BoomermanServer.Game;

namespace boomerman_server.Models.Walls
{
    public class Wall : IDataTransferable<WallDTO>
    {
        public Position Position { get; set; }
        public bool IsDestructible { get; set; }

        public WallDTO ToDTO()
        {
            return new WallDTO
            {
                IsDestructible = IsDestructible,
                Position = Position.ToDTO()
            };
        }
    }
}
