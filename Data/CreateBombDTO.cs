using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class CreateBombDTO
    {
        [JsonProperty("bombType")]
        public BombType BombType { get; set; }
    }
}
