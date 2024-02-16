using System;
using System.Net;
using System.Text.Json.Serialization;

namespace FastFeet.Application.Commons.Response;
public class Response
{
    protected Response(HttpStatusCode httpStatusCode)
        => HttpStatusCode = httpStatusCode;
    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; private set; }
}

