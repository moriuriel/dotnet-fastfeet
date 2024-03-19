using FastFeet.Application.Commons.Response;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;
using FastFeet.Infrastructure.ExternalService.Cryptography;
using MediatR;

namespace FastFeet.Application.Users.CreateUserCommand;

public sealed class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ResponseBase>
{
    private readonly ICryptographyService _cryptographyService;
    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(ICryptographyService cryptographyService, IUserRepository userRepository)
    {
        _cryptographyService = cryptographyService;
        _userRepository = userRepository;
    }

    public async Task<ResponseBase> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        if (!command.IsValid())
            return ErrorResponse.UnprocessableEntity(command.Errors);

        var user = command.User;

        var isValidEmail = await _userRepository.CheckExistsEmailAsync(user.Email, cancellationToken);
        if (!isValidEmail)
            return ErrorResponse.Conflict("Email already exists");

        var isValidTaxId = await _userRepository.CheckExistsTaxIdAsync(user.TaxId, cancellationToken);
        if (!isValidTaxId)
            return ErrorResponse.Conflict("TaxId already exists");

        var hasedPassword = _cryptographyService.ComputeSha256Hash(user.Password);

        var entity = User.Factory(
            name: user.Name,
            email: user.Email,
            password: hasedPassword,
            taxId: user.TaxId,
            type: user.UserType);

        if (entity.IsFailure)
            return ErrorResponse.UnprocessableEntity(
                entity.Errors.Select(errors => errors.Message).ToList());

        await _userRepository.SaveAsync(entity.Value, cancellationToken);

        return SuccessResponse.Created(entity.Value.Id);
    }
}