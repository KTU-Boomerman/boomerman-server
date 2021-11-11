using System.Threading.Tasks;
using BoomermanServer.Data;
using BoomermanServer.Game;

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
        Task UpdateLives(string playerId, int lives);
        Task UpdateScore(string playerId, int score);
        Task UpdateBombCount(string playerId, int count);
        Task PlacePowerup(PowerupDTO toDto);
        Task RemovePowerup(Position getPosition);
    }
}