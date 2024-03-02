using System.Net;
using FastFeet.Application.Commons.Response;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Infrastructure.ExternalService.Cryptography;
using FastFeet.Test.Unit.Commons;
using FluentAssertions;
using Moq;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public sealed class CreateUserHandlerTest
{
    private readonly Mock<ICryptographyService> _cryptographyService = new();
    
    [Fact]
    public async void ExecuteHandle_WithInvalidValues_ShouldReturnUnprocessableEntity()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new CreateUserCommandHandler(_cryptographyService.Object);

        var command = new CreateUserCommandBuilder()
            .WithEmail(email: FakerSingleton.GetInstance().Faker.Random.Word())
            .Build();

        //Act
        var result = await handler.Handle(command, cancellationToken);
        
        //Assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }

    [Fact] public async void ExecuteHandle_WithValidValues_ShouldReturnCreate()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new CreateUserCommandHandler(_cryptographyService.Object);
        
        var command = new CreateUserCommandBuilder().Build();

        var hashedPassword = FakerSingleton.GetInstance().Faker.Internet.Password();
        _cryptographyService.Setup(_ => _.ComputeSha256Hash(command.Password))
            .Returns(hashedPassword);
       
        //Act
        var result = await handler.Handle(command, cancellationToken);
        
        //Assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.Created);
        ((result as SuccessResponse)!).Id.Should().NotBeNull();
        
        _cryptographyService.Verify(_ => _.ComputeSha256Hash(command.Password), times: Times.Once);
    }
}