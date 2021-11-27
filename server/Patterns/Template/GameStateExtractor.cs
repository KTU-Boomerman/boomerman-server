using BoomermanServer.Data;
using BoomermanServer.Patterns.Facade;

namespace BoomermanServer.Patterns.Template
{
    public sealed class GameStateExtractor : DtoExtractor
    {
        public GameStateExtractor(ManagerFacade facade)
            : base(facade) { }

        public override object ExtractData()
        {
            return _facade.GameState;
        }

        public override object FormDto(object data)
        {
            return new GameStateDTO
            {
                GameState = data.ToString(),
            };
        }
    }
}
