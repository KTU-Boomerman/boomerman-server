using System.Threading.Tasks;
using BoomermanServer.Data;

namespace BoomermanServer.Hubs
{
    public interface IGameHub
    {
        Task Explosions(PositionDTO[] positionsDto);
        Task Notification(string title, string message);
        Task PlayerPlaceBomb(BombDTO bombDto);
        Task GameStateChange(GameStateDTO gameStateDto);
        Task PlayerMove(string playerId, PositionDTO positionDto);
        Task PlayerLeave(string playerId);
        Task Joined(PlayerDTO playerDto, PlayerDTO[] playersDto, GameStateDTO gameStateDto, MapDTO mapDto);
        Task PlayerJoin(PlayerDTO playerDto);
        Task UpdateLives(int lives);
    }
}