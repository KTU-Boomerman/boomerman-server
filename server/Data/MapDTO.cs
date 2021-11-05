using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class MapDTO
    {
        [JsonProperty("map")]
        public string[][] Map { get; set; }
    }
}