using BoomermanServer.Data;

namespace BoomermanServer.Game
{
	public class Player : IDataTransferable<PlayerDTO>
	{
		private string _id;
		private Position _position;


		public Player(string id, Position position)
		{
			_id = id;
			_position = position;
		}

		public string ID
		{
			get { return _id; }
		}

		public Position Position
		{
			get { return _position; }
			set { _position = value; }
		}

		public PlayerDTO ToDTO()
		{
			return new PlayerDTO()
			{
				ID = _id,
				Position = _position.ToDTO()
			};
		}

	}
}