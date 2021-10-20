using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BoomermanServer.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum BombType
    {
        [EnumMember(Value = "Regular")]
        Regular,
        [EnumMember(Value = "Wave")]
        Wave,
        [EnumMember(Value = "Boomerang")]
        Boomerang,
        [EnumMember(Value = "Pulse")]
        Pulse
    }
}
