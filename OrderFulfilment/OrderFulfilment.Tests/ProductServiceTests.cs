﻿using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Services;
using Xunit;

namespace WX.OrderFulfilment.Tests
{
	public class ProductServiceTests
	{

		private readonly Mock<IConfiguration> _configuration;

		public ProductServiceTests()
		{
			_configuration = new Mock<IConfiguration>();
		}

		[Fact]
		public void Should_Return_Products_Sorted_Low_To_High()
		{
			// Arrange
			var productService = new ProductService(_configuration.Object);
			var expectedProductsList = new List<Product>() {
				new Product { Name = "Test Product D", Price = 5.0m, Quantity = 0},
				new Product { Name = "Test Product C", Price = 10.99m, Quantity = 0},
				new Product { Name = "Test Product A", Price = 99.99m, Quantity = 0},
				new Product { Name = "Test Product B", Price = 101.99m, Quantity = 0},
				new Product { Name = "Test Product F", Price = 999999999999.0m, Quantity = 0},
			};

			var baseUrlConfigSection = new Mock<IConfigurationSection>();
			baseUrlConfigSection.Setup(a => a.Value).Returns("http://dev-wooliesx-recruitment.azurewebsites.net/");
			_configuration
				.Setup(config => config.GetSection("WooliesXUrls:BaseUrl"))
				.Returns(baseUrlConfigSection.Object);

			var productEndpointConfigSection = new Mock<IConfigurationSection>();
			productEndpointConfigSection.Setup(a => a.Value).Returns("api/resource/products");
			_configuration
				.Setup(config => config.GetSection("WooliesXUrls:ProductEndpoint"))
				.Returns(productEndpointConfigSection.Object);

			var tokenConfigSection = new Mock<IConfigurationSection>();
			tokenConfigSection.Setup(a => a.Value).Returns("276b32a9-8e35-4981-87b4-85e0a30f3319");
			_configuration
				.Setup(config => config.GetSection("UserDetails:Token"))
				.Returns(tokenConfigSection.Object);

			// Act
			var products = productService.GetProducts("low");

			// Assert
			expectedProductsList.Should().BeEquivalentTo(products);
		}
	}
}