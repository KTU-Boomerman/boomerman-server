using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BoomermanServer.Data
{
    public class GameStateDTO
    {
        [JsonProperty("gameState")]
        [JsonConverter(typeof(StringEnumConverter))]
        public string GameState { get; set; }
    }
}
