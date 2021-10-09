using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoomermanServer.Game;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace BoomermanServer.Data
{
    public class GameStateDTO
    {
        public string GameState { get; set; }   
    }
}