using AutoMapper;
using FluentAssertions;
using Moq;
using WX.OrderFulfilment.Controllers;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Repository;
using WX.OrderFulfilment.Resources;
using WX.OrderFulfilment.Services;
using Xunit;

namespace WX.OrderFulfilment.Tests
{
    public class UserDetailsControllerTests
    {
        private readonly Mock<IRepository> _mockRepo;
        private readonly UserDetailsController _controller;
        private readonly Mock<IUserDetailsService> _service;
        private readonly Mock<IMapper> _mapper;

        public UserDetailsControllerTests()
        {
            _mockRepo = new Mock<IRepository>();
            _service = new Mock<IUserDetailsService>();
            _controller = new UserDetailsController(_service.Object, _mapper.Object);
        }

        [Fact]
        public void Controller_Should_Return_Basic_Details()
        {
            // Arrange
            var expectedName = "Uday";
            var expectedToken = "12345-6789";
            _mockRepo.Setup(repo => repo.GetUserDetails())
                .Returns(new UserDetails() { Name = expectedName, Token = expectedToken });

            // Act
            var result = _controller.GetUserDetails();

            // Assert
            result.Should().BeOfType<UserDetailsResource>();
            ((UserDetailsResource)result).Name.Should().Be(expectedName);
            ((UserDetailsResource)result).Token.Should().Be(expectedToken);
        }
    }
}
