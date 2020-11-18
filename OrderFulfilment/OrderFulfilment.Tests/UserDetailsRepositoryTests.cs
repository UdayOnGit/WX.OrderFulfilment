using FluentAssertions;
using WX.OrderFulfilment.Repository;
using Xunit;

namespace WX.OrderFulfilment.Tests
{
    public class UserDetailsRepositoryTests
    {
        [Fact]
        public void Should_Return_Basic_Details()
        {
            // Arrange
            const string expectedName = "Uday";
            const string expectedToken = "1234-455662-22233333-3333";
            var repository = new OrderDetailsRepository();

            // Act
            var userDetails = repository.GetUserDetails();

            // Assert
            userDetails.Name.Should().Be(expectedName);
            userDetails.Token.Should().Be(expectedToken);
        }
    }
}
