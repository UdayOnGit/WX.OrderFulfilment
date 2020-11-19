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
			_ = GetShopperHistory();

			// Map string to enum
			Enum.TryParse(typeof(SortOptionEnum), sortOption, true, out var optionEnum);

			var products = GetProductsFromWXAPI().Result; //ToDo: Change this to async

			var result = optionEnum switch
			{
				SortOptionEnum.Low => GetProductsWithLowToHighPrice(products),
				SortOptionEnum.High => GetProductsWithHighToLowPrice(products),
				SortOptionEnum.Ascending => GetProductsWithAscendingName(products),
				SortOptionEnum.Descending => GetProductsWithDescendingName(products),
				SortOptionEnum.Recommended => GetRecommendedProducts(products),
				_ => Task.FromResult(Enumerable.Empty<Product>())
			};

			return result.Result;
		}

		private async Task<IEnumerable<Product>> GetProductsWithLowToHighPrice(IEnumerable<Product> products)
		{
			return products.OrderBy(product => product.Price);
		}

		private async Task<IEnumerable<Product>> GetProductsWithHighToLowPrice(IEnumerable<Product> products)
		{
			return products.OrderByDescending(product => product.Price);
		}

		private async Task<IEnumerable<Product>> GetProductsWithAscendingName(IEnumerable<Product> products)
		{
			return products.OrderBy(product => product.Name);
		}

		private async Task<IEnumerable<Product>> GetProductsWithDescendingName(IEnumerable<Product> products)
		{
			return products.OrderByDescending(product => product.Name);
		}

		private async Task<IEnumerable<Product>> GetRecommendedProducts(IEnumerable<Product> products)
		{
			return await GetPopularProducts(products);
		}

		private async Task<IEnumerable<Product>> GetProductsFromWXAPI()
		{
			var baseUrl = _configuration.GetValue<string>("WooliesXUrls:BaseUrl");
			var productEndpoint = _configuration.GetValue<string>("WooliesXUrls:ProductEndpoint");
			var token = _configuration.GetValue<string>("UserDetails:Token");

			var baseUri = new Uri(baseUrl);
			var productUri = new Uri(baseUri, $"{productEndpoint}?token={token}");
			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
				var request = new HttpRequestMessage(HttpMethod.Get, productUri);
				var response = await httpClient.SendAsync(request);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					var responseBody = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<IEnumerable<Product>>(responseBody);
				}
				else
				{
					// ToDo: Log error message
				}
			}
			return new List<Product>();
		}

		private async Task<IEnumerable<Product>> GetPopularProducts(IEnumerable<Product> products)
		{
			var shopperHistory = await GetShopperHistory();
			var userProducts = shopperHistory.Aggregate(new List<Product>(), (a, b) =>
			{
				if (b.Products != null)
				{
					a.AddRange(b.Products);
				}
				return a;
			});

			return products.Select(product =>
				new
				{
					Product = product,
					Populatiry = userProducts.Where(x => x.Name == product.Name).Count()
				})
				.OrderByDescending(p => p.Populatiry)
				.Select(p => p.Product);
		}

		private async Task<IEnumerable<ShopperHistory>> GetShopperHistory()
		{
			var baseUrl = _configuration.GetValue<string>("WooliesXUrls:BaseUrl");
			var productEndpoint = _configuration.GetValue<string>("WooliesXUrls:ShopperHistoryEndpoint");
			var token = _configuration.GetValue<string>("UserDetails:Token");

			var baseUri = new Uri(baseUrl);
			var productUri = new Uri(baseUri, $"{productEndpoint}?token={token}");

			using (var httpClient = new HttpClient())
			{
				httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
				var request = new HttpRequestMessage(HttpMethod.Get, productUri);
				var response = await httpClient.SendAsync(request);
				if (response.StatusCode == HttpStatusCode.OK)
				{
					var responseBody = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<IEnumerable<ShopperHistory>>(responseBody);
				}
				else
				{
					// ToDo: Log error message
				}
			}
			return new List<ShopperHistory>();
		}
	}
}
