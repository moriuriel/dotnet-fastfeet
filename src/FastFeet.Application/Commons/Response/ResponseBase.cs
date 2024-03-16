using System;
using System.Net;
using System.Text.Json.Serialization;

namespace FastFeet.Application.Commons.Response;
public class ResponseBase
{
    protected ResponseBase(HttpStatusCode httpStatusCode)
        => HttpStatusCode = httpStatusCode;
    [JsonIgnore]
    public HttpStatusCode HttpStatusCode { get; private set; }

    public bool IsSuccessStatusCode()
    {
        var statusCodeNumber = (int)HttpStatusCode;
        return statusCodeNumber >= 200 && statusCodeNumber <= 299;
    }
}

