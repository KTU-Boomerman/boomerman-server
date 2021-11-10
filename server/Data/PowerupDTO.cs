using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class PowerupDTO
    {
        [JsonProperty("position")] 
        public PositionDTO Position { get; set; }

        [JsonProperty("powerupType")]
        public PowerupType PowerupType { get; set; }
    }
}