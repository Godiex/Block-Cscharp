using System.Text.Json.Serialization;

namespace Base.Domain.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TypeUser
    {
        Admin,
        Customer
    }

}
