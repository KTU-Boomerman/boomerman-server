using BoomermanServer.Game;

namespace BoomermanServer.Patterns.Mediator
{
    public interface IChatroom
    {
        void Register(Player player);
        string Send(string from, string to, string message);
    }
}
