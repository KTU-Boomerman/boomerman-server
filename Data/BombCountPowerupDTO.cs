using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class BombCountPowerupDTO
    {
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }

        [JsonProperty("bombCountToAdd")]
        public int BombCountToAdd { get; set; }
    }
}
