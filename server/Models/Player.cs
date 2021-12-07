using BoomermanServer.Data;
using BoomermanServer.Patterns.Iterator;
using BoomermanServer.Patterns.Memento;
using BoomermanServer.Patterns.Mediator;

namespace BoomermanServer.Game
{
    public class Player : IDataTransferable<PlayerDTO>, IOriginator<PlayerDTO>
    {
        private int _lives;
        public string ID { get; private set; }
        public Position Position { get; set; }
        public double Speed { get; set; }
        public string Name { get; set; }

        public int MaxBombCount { get; set; }
        public bool IsImmortal { get; set; }
        public int _score;
        public ColorPalette ColorPalette { get; set; }

        public IChatroom Chatroom { get; set; }

        public Player(string id, Position position)
        {
            ID = id;
            Name = id;
            Position = position;
            Speed = 1.0;
            Lives = 3;
            MaxBombCount = 1;
            Score = 0;
            ColorPalette = new ColorPalette();
        }

        public int Lives
        {
            get { return _lives; }
            set
            {
                _lives = value switch
                {
                    > 3 => 3,
                    < 0 => 0,
                    _ => value
                };
            }
        }

        public int Score
        {
            get { return _score; }
            set
            {
                _score = value switch
                {
                    < 0 => 0,
                    _ => value
                };
            }
        }


        public PlayerDTO ToDTO()
        {
            return new PlayerDTO()
            {
                ID = this.ID,
                Position = this.Position.ToDTO(),
                Color = (PlayerColor)this.ColorPalette.GetIterator().CurrentItem(),
                Lives = this.Lives,
            };
        }

		public IMemento GetState()
		{
			return new PlayerMemento(this, this.ToDTO());
		}

        public void Restore(PlayerDTO state)
        {
            this.ID = state.ID;
            this.Position = new Position(state.Position);
            this._lives = state.Lives;
        }
	}
}
