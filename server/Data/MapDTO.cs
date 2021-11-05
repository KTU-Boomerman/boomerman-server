using System.Collections.Generic;
using BoomermanServer.Game;
using Newtonsoft.Json;

namespace BoomermanServer.Data
{
    public class MapDTO
    {
        [JsonProperty("walls")]
        public List<Position> Walls { get; set; }
    }
}