using System.Collections.Generic;
using BoomermanServer.Data;
using BoomermanServer.Game;
using BoomermanServer.Models.Bombs;
using BoomermanServer.Patterns.Factories;

namespace BoomermanServer.Patterns.Decorator
{
    public class BombCreator : Creator
    {
        private BombFactory _factory = new BombFactory();
        private BombType _type;
        public Dictionary<BombType, Bomb> Bombs { get; private set; }

        public BombCreator(BombType type)
        {
            _type = type;
            Bombs = new Dictionary<BombType, Bomb>();
        }

        public override void Execute()
        {
            if (Component != null && Component is BombCreator)
            {
                var bombComponent = Component as BombCreator;
                bombComponent.Execute();
                Bombs = bombComponent.Bombs;
            }
            if (!Bombs.ContainsKey(_type))
            {
                var bomb = _factory.CreateBomb(_type, new Position(0, 0));
                Bombs.Add(_type, bomb);
            }
        }
    }
}
