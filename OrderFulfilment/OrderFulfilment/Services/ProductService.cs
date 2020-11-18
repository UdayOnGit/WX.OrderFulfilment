using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
//using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IEnumerable<Product> GetProducts(string sortOption)
        {
            // Map string to enum
            Enum.TryParse(typeof(SortOptionEnum), sortOption, out var optionEnum);

            //_ = GetWooliesXProducts();

            var result = optionEnum switch
            {
                SortOptionEnum.Low => GetProductsWithLowToHighPrice(),
                SortOptionEnum.High => GetProductsWithHighToLowPrice(),
                SortOptionEnum.Ascending => GetProductsWithAscendingName(),
                SortOptionEnum.Descending => GetProductsWithDescendingName(),
                SortOptionEnum.Recommended => GetRecommendedProducts(),
                _ => new List<Product>()
            };

            return result;
        }

        public IEnumerable<Product> GetProductsWithLowToHighPrice()
        {
            var list = new List<Product>()
            {
                new Product(){ Name = "Test Product A", Price = 99.99m, Quantity = 3} ,
                new Product(){ Name = "Test Product B", Price = 101.99m, Quantity = 1} ,
                new Product(){ Name = "Test Product C", Price = 999999999999m, Quantity = 30} ,
            };
            return list;
            //return GetWooliesXProducts();
        }

        public IEnumerable<Product> GetProductsWithHighToLowPrice()
        {
            var list = new List<Product>()
            {
                new Product(){ Name = "Test Product A", Price = 99.99m, Quantity = 3} ,
                new Product(){ Name = "Test Product B", Price = 101.99m, Quantity = 1} ,
                new Product(){ Name = "Test Product C", Price = 999999999999m, Quantity = 30} ,
            };
            return list;
        }

        public IEnumerable<Product> GetProductsWithAscendingName()
        {
            var list = new List<Product>()
            {
                new Product(){ Name = "Test Product A", Price = 99.99m, Quantity = 3} ,
                new Product(){ Name = "Test Product B", Price = 101.99m, Quantity = 1} ,
                new Product(){ Name = "Test Product C", Price = 999999999999m, Quantity = 30} ,
            };
            return list;
        }

        public IEnumerable<Product> GetProductsWithDescendingName()
        {
            var list = new List<Product>()
            {
                new Product(){ Name = "Test Product A", Price = 99.99m, Quantity = 3} ,
                new Product(){ Name = "Test Product B", Price = 101.99m, Quantity = 1} ,
                new Product(){ Name = "Test Product C", Price = 999999999999m, Quantity = 30} ,
            };
            return list;
        }

        public IEnumerable<Product> GetRecommendedProducts()
        {
            var list = new List<Product>()
            {
                new Product(){ Name = "Test Product A", Price = 99.99m, Quantity = 3} ,
                new Product(){ Name = "Test Product B", Price = 101.99m, Quantity = 1} ,
                new Product(){ Name = "Test Product C", Price = 999999999999m, Quantity = 30} ,
            };
            return list;
        }

        public async Task<IEnumerable<Product>> GetWooliesXProducts()
        {
            //ToDo: Token is hardcoded in URL, it shouldn't be
            var baseUrl = _configuration.GetValue<string>("WooliesXUrls:ProductUrl");
            //using (var client = new HttpClient { BaseAddress = new Uri(baseUrl) })
            //{
            //    var request = new HttpRequestMessage(HttpMethod.Get, "api/component-test/mock");
            //    request.Headers.Add("Accept", "application/json");
            //    var response = await client.SendAsync(request);
            //    Console.WriteLine($"CreateResponse : {response.Content.ReadAsStringAsync().Result}");
            //    var status = response.StatusCode;
            //    Console.WriteLine($"Status : {status}");
            //    request.Dispose();
            //    response.Dispose();
            //    return status == HttpStatusCode.OK;
            //}
            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
                var request = new HttpRequestMessage(HttpMethod.Get, baseUrl);
                var response = await httpClient.SendAsync(request);
                //response.Content.ReadAsStringAsync().Result

                //var result = await JsonSerializer.DeserializeAsync<Product>(await response.Content.ReadAsStreamAsync());
            }
            return new List<Product>();
        }
    }
}
