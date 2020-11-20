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
            ConfigureTestSettings();
        }

        [Fact]
        public void Should_Return_Basic_Details()
        {
            // Arrange
            const string expectedName = "Uday Jadhav";
            const string expectedToken = "276b32a9-8e35-4981-87b4-85e0a30f3319";
            var repository = new OrderDetailsRepository(_configuration.Object);

            // Act
            var userDetails = repository.GetUserDetails();

            // Assert
            userDetails.Name.Should().Be(expectedName);
            userDetails.Token.Should().Be(expectedToken);
        }

        private void ConfigureTestSettings()
        {
            var tokenSection = new Mock<IConfigurationSection>();
            tokenSection.Setup(a => a.Value).Returns("276b32a9-8e35-4981-87b4-85e0a30f3319");
            _configuration
                .Setup(config => config.GetSection("UserDetails:Token"))
                .Returns(tokenSection.Object);

            var nameSection = new Mock<IConfigurationSection>();
            nameSection.Setup(a => a.Value).Returns("Uday Jadhav");
            _configuration
                .Setup(config => config.GetSection("UserDetails:Name"))
                .Returns(nameSection.Object);
        }
        }
}
