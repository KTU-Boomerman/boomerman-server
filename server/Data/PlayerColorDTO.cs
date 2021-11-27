using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class PlayerColorDTO
    {
        [JsonProperty("color")]
        public PlayerColor Color { get; set; }
    }
}
