using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class SpeedPowerupDTO
    {
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }

        [JsonProperty("speedPercentage")]
        public double SpeedPercentage { get; set; }
    }
}
