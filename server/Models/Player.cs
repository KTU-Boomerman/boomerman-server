using BoomermanServer.Data;

namespace BoomermanServer.Game
{
    public class Player : IDataTransferable<PlayerDTO>
    {
        private int _lives;
        public string ID { get; private set; }
        public Position Position { get; set; }
        public double Speed { get; set; }

        public int MaxBombCount { get; set; }
        public bool IsImmortal { get; set; }
        public int _score;

        public Player(string id, Position position)
        {
            ID = id;
            Position = position;
            Speed = 1.0;
            Lives = 3;
            MaxBombCount = 1;
            Score = 0;
        }

        public int Lives { 
            get { return _lives; }
            set
            {
                _lives = value switch {
                    > 3 => 3,
                    < 0 => 0,
                    _ => value
                };
            }
        }
        
        public int Score { 
            get { return _score; }
            set
            {
                _score = value switch {
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
                Position = this.Position.ToDTO()
            };
        }
    }
}
