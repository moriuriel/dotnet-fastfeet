using System.Text.Json.Serialization;

namespace FastFeet.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum UserType
{
    None,
    Customer,
    Deliveryman
}


