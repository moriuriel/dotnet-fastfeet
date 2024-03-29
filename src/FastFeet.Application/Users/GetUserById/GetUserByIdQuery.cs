﻿using MediatR;
using FastFeet.Application.Commons.Response;
using FastFeet.Application.Commons.Command;

namespace FastFeet.Application.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : CommandBase, IRequest<ResponseBase>
{
    public override bool IsValid()
    {
        ValidationResult = new GetUserByIdValidator().Validate(this);
        return ValidationResult.IsValid;
    }
}