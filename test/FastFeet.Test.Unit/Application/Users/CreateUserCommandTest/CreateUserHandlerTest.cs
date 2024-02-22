using System.Net;
using FastFeet.Application.Users.CreateUserCommand;
using FastFeet.Test.Unit.Commons;
using FluentAssertions;

namespace FastFeet.Test.Unit.Application.Users.CreateUserCommandTest;

public sealed class CreateUserHandlerTest
{
    [Fact]
    public async void ExecuteHandle_WithInvalidValues_ShouldReturnUnprocessableEntity()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new CreateUserCommandHandler();

        var command = new CreateUserCommandBuilder()
            .WithEmail(email: FakerSingleton.GetInstance().Faker.Random.Word())
            .Build();

        //Act
        var result = await handler.Handle(command, cancellationToken);
        
        //Assert
        result.HttpStatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);
    }
}