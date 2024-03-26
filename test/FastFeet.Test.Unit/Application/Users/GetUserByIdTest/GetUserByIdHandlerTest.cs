using System.Net;
using FastFeet.Application.Users.GetUserById;
using FastFeet.Domain.Entities;
using FastFeet.Domain.Interfaces.Repository;
using FastFeet.Test.Unit.Commons.Builders;
using FluentAssertions;
using Moq;

namespace FastFeet.Test.Unit.Application.Users.GetUserByIdTest;

public class GetUserByIdHandlerTest
{
    private readonly Mock<IUserRepository> _userRepository = new();

    [Fact]
    public async Task ExecuteMethodHandle_WithInvalidValues_ShouldReturnUnprocessableEntity()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new GetUserByIdHandler(_userRepository.Object);

        var query = new GetUserByIdQueryBuilder().WithEmptyUserId().Build();

        //Act
        var result = await handler.Handle(query, cancellationToken);

        //Arrange
        result.HttpStatusCode.Should().Be(HttpStatusCode.UnprocessableEntity);

        _userRepository.Verify(
            _ => _.FindByIdAsync(It.IsAny<Guid>(), cancellationToken),
            times: Times.Never);
    }

    [Fact]
    public async Task ExecuteMethodHandle_WithNotFoundUserId_ShouldReturnNoContent()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var handler = new GetUserByIdHandler(_userRepository.Object);

        var query = new GetUserByIdQueryBuilder().Build();

        //Act
        var result = await handler.Handle(query, cancellationToken);

        //Arrange
        result.HttpStatusCode.Should().Be(HttpStatusCode.NoContent);

        _userRepository.Verify(
            _ => _.FindByIdAsync(query.UserId, cancellationToken),
            times: Times.Once);
    }

    [Fact]
    public async Task ExecuteMethodHandle_WithValidValues_ShouldReturnOk()
    {
        //Arrange
        var cancellationToken = CancellationToken.None;

        var user = new UserBuilder().Build();

        var handler = new GetUserByIdHandler(_userRepository.Object);

        var query = new GetUserByIdQueryBuilder().Build();
        _userRepository.Setup(
           _ => _.FindByIdAsync(query.UserId, cancellationToken))
            .ReturnsAsync(user);

        //Act
        var result = await handler.Handle(query, cancellationToken);

        //Arrange
        result.HttpStatusCode.Should().Be(HttpStatusCode.OK);
        (result as GetUserByIdResponse)!.User.Should().NotBeNull();

        _userRepository.Verify(
            _ => _.FindByIdAsync(query.UserId, cancellationToken),
            times: Times.Once);
    }
}
