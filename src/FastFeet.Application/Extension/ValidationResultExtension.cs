using FluentValidation.Results;

namespace FastFeet.Application.Extension;

public static class FluentValidationExtension
{
    public static List<string> GetResultErrors(this ValidationResult validationResult)
    {
        return validationResult.Errors.Select(_ => _.ErrorMessage).ToList();
    }
}


