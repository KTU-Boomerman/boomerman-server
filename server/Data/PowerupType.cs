using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BoomermanServer.Data
{
    [Newtonsoft.Json.JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PowerupType
    {
        [EnumMember(Value = "BigHealth")]
        BigHealth,
        [EnumMember(Value = "SmallHealth")]
        SmallHealth,
        [EnumMember(Value = "BigSpeed")]
        BigSpeed,
        [EnumMember(Value = "SmallSpeed")]
        SmallSpeed,
        [EnumMember(Value = "BigBomb")]
        BigBomb,
        [EnumMember(Value = "SmallBomb")]
        SmallBomb
    }
}