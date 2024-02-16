using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Entities;
using MediatR;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
{
    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return ErrorResponse.UnprocessableEntity(request.Errors);

        var entity = User.Factory(
            name: request.Name,
            email: request.Email,
            password: request.Password,
            taxId: request.TaxId,
            type: request.UserType);

        if(entity.IsFailure)
            return ErrorResponse.UnprocessableEntity(entity.Errors.Select(_ => _.Message).ToList());

        return SuccessResponse.Created(entity.Value.Id);
    }
}