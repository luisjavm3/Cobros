using System.Text.Json.Serialization;

namespace Cobros.API.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Gender
    {
        MALE,
        FEMALE
    }
}
