using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public class ProductService : IProductService
	{
        private readonly Func<SortOptionEnum, IGetProducts> _productsRetriever;

        public ProductService(Func<SortOptionEnum, IGetProducts> productsRetriever)
		{
            _productsRetriever = productsRetriever;
        }

		public async Task<IEnumerable<Product>> GetProducts(SortOptionEnum sortOption)
		{
			return await _productsRetriever(sortOption).GetProductsAsync();

		}

	}
}
