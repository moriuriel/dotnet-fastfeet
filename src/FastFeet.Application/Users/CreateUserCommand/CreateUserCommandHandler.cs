using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;
using FastFeet.Infrastructure.ExternalService.Cryptography;
using MediatR;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, Response>
{
    private readonly ICryptographyService _cryptographyService;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(ICryptographyService cryptographyService, IUserRepository userRepository)
    {
        _cryptographyService = cryptographyService;
        _userRepository = userRepository;
    }

    public async Task<Response> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.IsValid())
            return ErrorResponse.UnprocessableEntity(request.Errors);
        
        var isValidEmail = await _userRepository.CheckHasEmail(request.Email);
        if (!isValidEmail)
            return ErrorResponse.Conflict("Email already exists");

        var isValidTaxId = await _userRepository.CheckHasTaxId(request.TaxId);
        if (!isValidTaxId)
            return ErrorResponse.Conflict("TaxId already exists");
        
        var hasedPassword = _cryptographyService.ComputeSha256Hash(request.Password);
        
        var entity = User.Factory(
            name: request.Name,
            email: request.Email,
            password: hasedPassword,
            taxId: request.TaxId,
            type: request.UserType);

        if(entity.IsFailure)
            return ErrorResponse.UnprocessableEntity(
                entity.Errors.Select(errors => errors.Message).ToList());
        
        await _userRepository.Create(entity.Value);
        
        return SuccessResponse.Created(entity.Value.Id);
    }
}