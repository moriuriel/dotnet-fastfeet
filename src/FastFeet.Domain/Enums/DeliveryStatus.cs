﻿using System.Text.Json.Serialization;

namespace FastFeet.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeliveryStatus
{
    None,
    NotAvailable,
    Available,
    Accepted,
    Completed,
    Cancelled,
}