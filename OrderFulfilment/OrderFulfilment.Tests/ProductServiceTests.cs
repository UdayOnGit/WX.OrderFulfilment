﻿using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Moq;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Services;
using Xunit;
using System.Threading.Tasks;

namespace WX.OrderFulfilment.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IConfiguration> _configuration;

        public ProductServiceTests()
        {
            _configuration = new Mock<IConfiguration>();
            ConfigureTestSettings();
        }

        [Fact]
        public async Task FakeTest()
        {
            // Arrange
            static IGetProducts productRetriever(SortOptionEnum sortOption)
            {
                var getWooliesProducts = new Mock<IGetWooliesProducts>();
                return new GetProductsSortedLowToHigh(getWooliesProducts.Object);
            }

            var productService = new ProductService(productRetriever);

            // Act
            var result = await productService.GetProducts(SortOptionEnum.Low);

            // Assert
            result.Should().NotBeNull();
        }

        [Theory(Skip = "Modify Test")]
        [ClassData(typeof(ProductTestData))]
        public async Task Should_Return_Products_Sorted_By_Condition(SortOptionEnum sortOption, List<Product> expectedProductsList)
        {
            // // Arrange
            // var productService = new ProductService(_configuration.Object);

            // // Act
            // var products = await productService.GetProducts(sortOption);

            // // Assert
            // expectedProductsList.Should().BeEquivalentTo(products, options => options.WithStrictOrdering());
        }

        [Theory(Skip = "Modify Test")]
        [ClassData(typeof(LowSortOptionTestData))]
        public async Task Should_Return_Products_For_Case_Insensitive_Low_Sortoption(SortOptionEnum sortOption, List<Product> expectedProductsList)
        {
            // Arrange
            // var productService = new ProductService(_configuration.Object);

            // // Act
            // var products = await productService.GetProducts(sortOption);

            // // Assert
            // expectedProductsList.Should().BeEquivalentTo(products, options => options.WithStrictOrdering());
        }

        [Theory(Skip = "Modify Test")]
        [ClassData(typeof(HighSortOptionTestData))]
        public async Task Should_Return_Products_For_Case_Insensitive_High_Sortoption(SortOptionEnum sortOption, List<Product> expectedProductsList)
        {
            // // Arrange
            // var productService = new ProductService(_configuration.Object);

            // // Act
            // var products = await productService.GetProducts(sortOption);

            // // Assert
            // expectedProductsList.Should().BeEquivalentTo(products, options => options.WithStrictOrdering());
        }

        [Theory(Skip = "Modify Test")]
        [ClassData(typeof(AscendingSortOptionTestData))]
        public async Task Should_Return_Products_For_Case_Insensitive_Ascending_Sortoption(SortOptionEnum sortOption, List<Product> expectedProductsList)
        {
            // // Arrange
            // var productService = new ProductService(_configuration.Object);

            // // Act
            // var products = await productService.GetProducts(sortOption);

            // // Assert
            // expectedProductsList.Should().BeEquivalentTo(products, options => options.WithStrictOrdering());
        }

        [Theory(Skip = "Modify Test")]
        [ClassData(typeof(DescendingSortOptionTestData))]
        public async Task Should_Return_Products_For_Case_Insensitive_Descending_Sortoption(SortOptionEnum sortOption, List<Product> expectedProductsList)
        {
            // // Arrange
            // var productService = new ProductService(_configuration.Object);

            // // Act
            // var products = await productService.GetProducts(sortOption);

            // // Assert
            // expectedProductsList.Should().BeEquivalentTo(products, options => options.WithStrictOrdering());
        }

        [Theory(Skip = "Modify Test")]
        [ClassData(typeof(RecommendedSortOptionTestData))]
        public async Task Should_Return_Products_For_Case_Insensitive_Recommended_Sortoption(SortOptionEnum sortOption, List<Product> expectedProductsList)
        {
            // // Arrange
            // var productService = new ProductService(_configuration.Object);

            // // Act
            // var products = await productService.GetProducts(sortOption);

            // // Assert
            // expectedProductsList.Should().BeEquivalentTo(products, options => options.WithStrictOrdering());
        }

        private class ProductTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SortOptionEnum.Low, ProductStub.ProductsSortedByLowPrice };
                yield return new object[] { SortOptionEnum.High, ProductStub.ProductsSortedByHighPrice };
                yield return new object[] { SortOptionEnum.Ascending, ProductStub.ProductsSortedByAscending };
                yield return new object[] { SortOptionEnum.Descending, ProductStub.ProductsSortedByDescending };
                yield return new object[] { SortOptionEnum.Recommended, ProductStub.ProductsSortedByPopularity };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class LowSortOptionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SortOptionEnum.Low, ProductStub.ProductsSortedByLowPrice };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class HighSortOptionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SortOptionEnum.High, ProductStub.ProductsSortedByHighPrice };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class AscendingSortOptionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SortOptionEnum.Ascending, ProductStub.ProductsSortedByAscending };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class DescendingSortOptionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SortOptionEnum.Descending, ProductStub.ProductsSortedByDescending };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class RecommendedSortOptionTestData : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { SortOptionEnum.Descending, ProductStub.ProductsSortedByDescending };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ProductStub
        {
            private static readonly Product ProductA = new Product { Name = "Test Product A", Price = 99.99m, Quantity = 0 };
            private static readonly Product ProductB = new Product { Name = "Test Product B", Price = 101.99m, Quantity = 0 };
            private static readonly Product ProductC = new Product { Name = "Test Product C", Price = 10.99m, Quantity = 0 };
            private static readonly Product ProductD = new Product { Name = "Test Product D", Price = 5.0m, Quantity = 0 };
            private static readonly Product ProductF = new Product { Name = "Test Product F", Price = 999999999999.0m, Quantity = 0 };

            public static List<Product> ProductsSortedByLowPrice = new List<Product>() {
                ProductD,
                ProductC,
                ProductA,
                ProductB,
                ProductF };

            public static List<Product> ProductsSortedByHighPrice = new List<Product>() {
                ProductF,
                ProductB,
                ProductA,
                ProductC,
                ProductD };

            public static List<Product> ProductsSortedByAscending = new List<Product>() {
                ProductA,
                ProductB,
                ProductC,
                ProductD,
                ProductF};

            public static List<Product> ProductsSortedByDescending = new List<Product>() {
                ProductF,
                ProductD,
                ProductC,
                ProductB,
                ProductA };

            public static List<Product> ProductsSortedByPopularity = new List<Product>() {
                ProductA,
                ProductB,
                ProductF,
                ProductC,
                ProductD };
        }

        private void ConfigureTestSettings()
        {
            var baseUrlSection = new Mock<IConfigurationSection>();
            baseUrlSection.Setup(a => a.Value).Returns("http://dev-wooliesx-recruitment.azurewebsites.net/");
            _configuration
                .Setup(config => config.GetSection("WooliesXUrls:BaseUrl"))
                .Returns(baseUrlSection.Object);

            var productEndpointSection = new Mock<IConfigurationSection>();
            productEndpointSection.Setup(a => a.Value).Returns("api/resource/products");
            _configuration
                .Setup(config => config.GetSection("WooliesXUrls:ProductEndpoint"))
                .Returns(productEndpointSection.Object);

            var tokenSection = new Mock<IConfigurationSection>();
            tokenSection.Setup(a => a.Value).Returns("276b32a9-8e35-4981-87b4-85e0a30f3319");
            _configuration
                .Setup(config => config.GetSection("UserDetails:Token"))
                .Returns(tokenSection.Object);

            var shopperHistoryEndpointSection = new Mock<IConfigurationSection>();
            shopperHistoryEndpointSection.Setup(a => a.Value).Returns("api/resource/shopperHistory");
            _configuration
                .Setup(config => config.GetSection("WooliesXUrls:ShopperHistoryEndpoint"))
                .Returns(shopperHistoryEndpointSection.Object);
        }
    }
}
