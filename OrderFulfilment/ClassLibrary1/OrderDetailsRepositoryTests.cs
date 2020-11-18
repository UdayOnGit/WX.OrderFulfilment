using FluentAssertions;
using OrderFulfilment.Repository;
using Xunit;

namespace ClassLibrary1
{
    public class OrderDetailsRepositoryTests
    {
        [Fact]
        public void Should_Return_Basic_Details()
        {
            // Arrange
            const string expectedName = "Uday";
            const string expectedToken = "1234-455662-22233333-3333";
            var repository = new OrderDetailsRepository();

            // Act
            var basicDetails = repository.GetBasicDetail();

            // Assert
            basicDetails.Name.Should().Be(expectedName);
            basicDetails.Token.Should().Be(expectedToken);
        }
    }
}
