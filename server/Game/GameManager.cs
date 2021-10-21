namespace BoomermanServer.Game
{
    public class GameManager : IGameManager
    {
        public GameState GameState { get; set; }

        public GameManager()
        {
            GameState = GameState.GameInProgress;
        }

        public int GetMinPlayers()
        {
            return 1;
        }

        public void StartGame()
        {
            GameState = GameState.GameInProgress;
        }
    }
}
