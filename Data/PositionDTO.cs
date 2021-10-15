using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class PositionDTO
    {
        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }
    }
}
