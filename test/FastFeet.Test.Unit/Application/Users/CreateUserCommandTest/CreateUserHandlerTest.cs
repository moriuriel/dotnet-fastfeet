using System.Net;
using FastFeet.Application.Commons.Response;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;
using FastFeet.Infrastructure.ExternalService.Cryptography;
using FastFeet.Test.Unit.Commons;
using FluentAssertions;
using Moq;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public sealed class CreateUserHandlerTest
{
    private readonly Mock<ICryptographyService> _cryptographyService = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    
    [Fact]
    public async void ExecuteHandle_WithInvalidValues_ShouldReturnUnprocessableEntity()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new CreateUserCommandHandler(_cryptographyService.Object, _userRepository.Object);

        var user = new CreateUserRequestBuilder().WithEmail(email: FakerSingleton.GetInstance().Faker.Random.Word()).Build();

        var command = new CreateUserCommandBuilder()
            .WithUser(user)
            .Build();

        //Act
        var result = await handler.Handle(command, cancellationToken);
        
        //Assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        _userRepository.Verify(_ => _.CheckExistsEmailAsync(It.IsAny<string>(), cancellationToken), times: Times.Never);

        _userRepository.Verify(_ => _.CheckExistsTaxIdAsync(It.IsAny<string>(), cancellationToken), times: Times.Never);

        _cryptographyService.Verify(_ => _.ComputeSha256Hash(command.User.Password), times: Times.Never);

        _userRepository.Verify(_ => _.SaveAsync(It.IsAny<User>(), cancellationToken), times: Times.Never);
    }

    [Fact]
    public async void ExecuteHandle_WithValidValues_ShouldReturnCreate()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new CreateUserCommandHandler(_cryptographyService.Object, _userRepository.Object);
        
        var command = new CreateUserCommandBuilder().Build();

        var hashedPassword = FakerSingleton.GetInstance().Faker.Internet.Password();
        _cryptographyService.Setup(_ => _.ComputeSha256Hash(command.User.Password))
            .Returns(hashedPassword);

        var isValidEmail = true;
        _userRepository.Setup(_ => _.CheckExistsEmailAsync(command.User.Email, cancellationToken))
            .ReturnsAsync(isValidEmail);

        var isValidTaxId = true;
        _userRepository.Setup(_ => _.CheckExistsTaxIdAsync(command.User.TaxId, cancellationToken))
            .ReturnsAsync(isValidTaxId);
        //Act
        var result = await handler.Handle(command, cancellationToken);
        
        //Assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.Created);
        ((result as SuccessResponse)!).Id.Should().NotBeNull();
        
        _cryptographyService.Verify(_ => _.ComputeSha256Hash(command.User.Password), times: Times.Once);

        _userRepository.Verify(_ => _.CheckExistsEmailAsync(It.IsAny<string>(),cancellationToken), times: Times.Once);

        _userRepository.Verify(_ => _.CheckExistsTaxIdAsync(It.IsAny<string>(), cancellationToken), times: Times.Once);

        _userRepository.Verify(_ => _.SaveAsync(It.IsAny<User>(),cancellationToken), times: Times.Once);
    }
}