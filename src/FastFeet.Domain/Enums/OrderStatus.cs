using System.Text.Json.Serialization;

namespace FastFeet.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum OrderStatus
{
    None,
    ToDo,
    Doing,
    Delivery,
    Complete,
    Ready,
    Cancelled,
}