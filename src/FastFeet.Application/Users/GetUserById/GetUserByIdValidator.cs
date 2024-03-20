using System;
using FluentValidation;

namespace FastFeet.Application.Users.GetUserById
{
    public class GetUserByIdValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdValidator()
        {
            RuleFor(_ => _.UserId)
                .NotEmpty()
                .Must(userId => Guid.TryParse(userId.ToString(), out var newGuid));
        }
    }
}

