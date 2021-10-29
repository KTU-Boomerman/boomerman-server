using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class PositionValidationDTO
    {
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }
    }
}
