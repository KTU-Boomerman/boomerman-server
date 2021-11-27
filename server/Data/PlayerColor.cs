using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace BoomermanServer.Data
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PlayerColor
    {
        [EnumMember(Value = "Red")]
        Red,
        [EnumMember(Value = "Green")]
        Green,
        [EnumMember(Value = "Blue")]
        Blue
    }
}
