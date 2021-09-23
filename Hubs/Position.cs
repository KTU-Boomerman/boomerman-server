using Newtonsoft.Json;

namespace BoomermanServer.Hubs
{
    public class Player 
    {
      [JsonProperty("id")]
      public int ID { get; set; }
      [JsonProperty("name")]
      public string Name { get; set; }
      [JsonProperty("x")]
      public double X { get; set; }
      [JsonProperty("y")]
      public double Y { get; set; }
    }
}