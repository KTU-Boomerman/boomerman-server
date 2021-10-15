using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class PositionValidationDTO
    {
        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
        [JsonProperty("position")]
        public PositionDTO Position { get; set; }
    }
}
