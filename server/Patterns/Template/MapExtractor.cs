using System.Collections.Generic;
using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Patterns.Facade;

namespace BoomermanServer.Patterns.Template
{
    public sealed class MapExtractor : DtoExtractor
    {
        public MapExtractor(ManagerFacade facade)
            : base(facade) { }

        public override object ExtractData()
        {
            return _facade.GetDestructibleWalls();
        }

        public override object FormDto(object data)
        {
            return new MapDTO
            {
                Walls = data as List<Position>,
            };
        }
    }
}
