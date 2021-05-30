using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

		public async Task<IEnumerable<Product>> GetProducts(SortOptionEnum sortOption)
		{
			var result = sortOption switch
			{
				SortOptionEnum.Low => GetProductsWithLowToHighPrice(),
				SortOptionEnum.High => GetProductsWithHighToLowPrice(),
				SortOptionEnum.Ascending => GetProductsWithAscendingName(),
				SortOptionEnum.Descending => GetProductsWithDescendingName(),
				SortOptionEnum.Recommended => GetRecommendedProducts(),
				_ => Task.FromResult(Enumerable.Empty<Product>())
			};

			return await result;
		}

		#region Get products based on the sort option
		private async Task<IEnumerable<Product>> GetProductsWithLowToHighPrice()
		{
			var products = await GetProductsFromWXAPI();
			return products.OrderBy(product => product.Price);
		}

		private async Task<IEnumerable<Product>> GetProductsWithHighToLowPrice()
		{
			var products = await GetProductsFromWXAPI();
			return products.OrderByDescending(product => product.Price);
		}

		private async Task<IEnumerable<Product>> GetProductsWithAscendingName()
		{
			var products = await GetProductsFromWXAPI();
			return products.OrderBy(product => product.Name);
		}

		private async Task<IEnumerable<Product>> GetProductsWithDescendingName()
		{
			var products = await GetProductsFromWXAPI();
			return products.OrderByDescending(product => product.Name);
		}

		private async Task<IEnumerable<Product>> GetRecommendedProducts()
		{
			return await GetPopularProducts();
		}

		private async Task<IEnumerable<Product>> GetPopularProducts()
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

			var products = await GetProductsFromWXAPI();
			return products.Select(product =>
				new
				{
					Product = product,
					Populatiry = userProducts.Where(x => x.Name == product.Name).Count()
				})
				.OrderByDescending(p => p.Populatiry)
				.Select(p => p.Product);
		}
		#endregion

		#region Get Woolies resources

		// ToDo: Getting resources from external api seems tightly coupled with ProductService. Need to bring it out
		private async Task<IEnumerable<Product>> GetProductsFromWXAPI()
		{
			var baseUrl = _configuration.GetValue<string>("WooliesXUrls:BaseUrl");
			var productEndpoint = _configuration.GetValue<string>("WooliesXUrls:ProductEndpoint");
			var token = _configuration.GetValue<string>("UserDetails:Token");

			var baseUri = new Uri(baseUrl);
			var productUri = new Uri(baseUri, $"{productEndpoint}?token={token}");
			using (var httpClient = new HttpClient())	// ToDo: This can be taken out into a separate helper class
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

		#endregion

	}
}
