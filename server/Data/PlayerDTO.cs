using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class PlayerDTO
    {
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }
        [JsonProperty("color")]
        public PlayerColor Color { get; set; }
        [JsonProperty("lives")]
        public int Lives { get; set; }
    }
}
