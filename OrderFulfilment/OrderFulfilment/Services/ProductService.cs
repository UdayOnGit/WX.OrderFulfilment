﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public class ProductService : IProductService
	{
		private readonly IConfiguration _configuration;
		private readonly IGetWooliesProducts _getWooliesProducts;

		public ProductService(IConfiguration configuration,
		IGetWooliesProducts getWooliesProducts)
		{
			_configuration = configuration;
			_getWooliesProducts = getWooliesProducts;
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
			var products = await _getWooliesProducts.GetProductsFromWXAPI();
			return products.OrderBy(product => product.Price);
		}

		private async Task<IEnumerable<Product>> GetProductsWithHighToLowPrice()
		{
			var products = await _getWooliesProducts.GetProductsFromWXAPI();
			return products.OrderByDescending(product => product.Price);
		}

		private async Task<IEnumerable<Product>> GetProductsWithAscendingName()
		{
			var products = await _getWooliesProducts.GetProductsFromWXAPI();
			return products.OrderBy(product => product.Name);
		}

		private async Task<IEnumerable<Product>> GetProductsWithDescendingName()
		{
			var products = await _getWooliesProducts.GetProductsFromWXAPI();
			return products.OrderByDescending(product => product.Name);
		}

		private async Task<IEnumerable<Product>> GetRecommendedProducts()
		{
			return await GetPopularProducts();
		}

		private async Task<IEnumerable<Product>> GetPopularProducts()
		{
			var shopperHistory = await _getWooliesProducts.GetShopperHistory();
			var userProducts = shopperHistory.Aggregate(new List<Product>(), (a, b) =>
			{
				if (b.Products != null)
				{
					a.AddRange(b.Products);
				}
				return a;
			});

			var products = await _getWooliesProducts.GetProductsFromWXAPI();
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
	}
}
