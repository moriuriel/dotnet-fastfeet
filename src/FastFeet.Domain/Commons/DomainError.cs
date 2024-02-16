using FluentValidation.Results;

namespace FastFeet.Domain.Commons;

public static class DomainError
{
    public static IEnumerable<Error> GetErrors(IReadOnlyList<ValidationFailure> errors)
        => errors.Select(error => new Error(
                code: nameof(DomainError),
                message: error.ErrorMessage
            ));
}