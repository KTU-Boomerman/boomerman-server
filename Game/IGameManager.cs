namespace BoomermanServer.Game
{
    public interface IGameManager
    {
        GameState GameState { get; set; }
        int GetMinPlayers();

        void StartGame();
    }
}
