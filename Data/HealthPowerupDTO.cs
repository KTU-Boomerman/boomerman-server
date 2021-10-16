using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class HealthPowerupDTO
    {
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }

        [JsonProperty("healthAmount")]
        public int HealthAmount { get; set; }
    }
}
