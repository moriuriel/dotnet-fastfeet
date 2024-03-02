using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Entities;
using FastFeet.Infrastructure.ExternalService.Cryptography;
using MediatR;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
{
    private readonly ICryptographyService _cryptographyService;

    public CreateUserCommandHandler(ICryptographyService cryptographyService)
    {
        _cryptographyService = cryptographyService;
    }

    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return ErrorResponse.UnprocessableEntity(request.Errors);

        var hasedPassword = _cryptographyService.ComputeSha256Hash(request.Password);
        
        var entity = User.Factory(
            name: request.Name,
            email: request.Email,
            password: hasedPassword,
            taxId: request.TaxId,
            type: request.UserType);

        if(entity.IsFailure)
            return ErrorResponse.UnprocessableEntity(entity.Errors.Select(_ => _.Message).ToList());

        return SuccessResponse.Created(entity.Value.Id);
    }
}