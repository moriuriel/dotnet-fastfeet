using System.Text.Json.Serialization;
using FastFeet.Application.Extension;
using FluentValidation.Results;

namespace FastFeet.Application.Commons.Command;
public abstract record CommandBase
{
    [JsonIgnore]
    public ValidationResult ValidationResult { get; protected set; }

    [JsonIgnore]
    public List<string> Errors => ValidationResult.GetResultErrors();

    protected CommandBase()
    {
        ValidationResult = new ValidationResult(); 
    }

    public abstract bool IsValid();
}
