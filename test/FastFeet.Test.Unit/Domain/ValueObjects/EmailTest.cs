using FastFeet.Domain.ValueObjects;
using FluentAssertions;

namespace FastFeet.Test.Unit.Domain.ValueObjects;
public class EmailTest
{
    [Theory]
    [InlineData("valid@email.com", true)]
    [InlineData("invalid-email", false)]
    public void CreateEmail_WithValues_ShouldReturnExpectedResult(string email, bool isValid)
    {
        //Arrange Act
        var emailVo = Email.Create(email);
        
        //Assert
        if (isValid)
        {
            emailVo.IsSuccess.Should().BeTrue();
            emailVo.IsFailure.Should().BeFalse();
            emailVo.Value.Value.Should().Be(email);
        }
        else
        {
            emailVo.IsSuccess.Should().BeFalse();
            emailVo.IsFailure.Should().BeTrue();
            emailVo.Errors.Should().NotBeNull();
        }
    }
}