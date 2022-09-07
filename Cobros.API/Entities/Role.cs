using System.Text.Json.Serialization;

namespace Cobros.API.Entities
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        USER,
        ADMIN
    }
}
