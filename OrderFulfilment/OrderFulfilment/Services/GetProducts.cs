using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public class GetProducts : IGetProducts
    {
        private readonly IConfiguration _configuration;

        public GetProducts(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Product>> GetProductsFromWXAPI()
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

        public async Task<IEnumerable<ShopperHistory>> GetShopperHistory()
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