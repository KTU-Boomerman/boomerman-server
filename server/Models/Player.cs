using BoomermanServer.Data;

namespace BoomermanServer.Game
{
    public class Player : IDataTransferable<PlayerDTO>
    {
        public string ID { get; private set; }
        public Position Position { get; set; }
        public double Speed { get; set; }
        public int Health { get; set; }
        public int MaxBombCount { get; set; }
        public bool IsImmortal { get; set; }

        public Player(string id, Position position)
        {
            ID = id;
            Position = position;
            Speed = 1.0;
            Health = 100;
            MaxBombCount = 1;
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
