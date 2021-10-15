using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class BombDTO
    {
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }
    }
}
