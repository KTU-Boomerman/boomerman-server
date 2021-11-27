using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoomermanServer.Patterns.ChainOfResponsibility
{
    public class Message
    {
        public string PlayerID { get; set; }
        public string PlayerName { get; set; }
        public string Text { get; set; }
    }
}