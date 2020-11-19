using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using WX.OrderFulfilment.Repository;
using Xunit;

namespace WX.OrderFulfilment.Tests
{
    public class UserDetailsRepositoryTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public UserDetailsRepositoryTests()
        {
            _configuration = new Mock<IConfiguration>();
        }

        [Fact]
        public void Should_Return_Basic_Details()
        {
            // Arrange
            const string expectedName = "Uday";
            const string expectedToken = "1234-455662-22233333-3333";
            var repository = new OrderDetailsRepository(_configuration.Object);

            // Act
            var userDetails = repository.GetUserDetails();

            // Assert
            userDetails.Name.Should().Be(expectedName);
            userDetails.Token.Should().Be(expectedToken);
        }
    }
}
