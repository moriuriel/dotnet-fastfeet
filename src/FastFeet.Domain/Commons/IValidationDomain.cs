using FluentValidation.Results;

namespace FastFeet.Domain.Commons;

public interface IValidationDomain
{
    ValidationResult GetValidationResult();
}