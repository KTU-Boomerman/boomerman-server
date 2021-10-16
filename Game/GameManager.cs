namespace BoomermanServer.Game
{
    public class GameManager : IGameManager
    {
        private GameState _gameState;
        public GameState GameState { get => _gameState; set => _gameState = value; }

        public GameManager()
        {
            _gameState = GameState.GameInProgress;
        }

        public int GetMinPlayers()
        {
            return 1;
        }

        public void StartGame()
        {
            _gameState = GameState.GameInProgress;
        }
    }
}
