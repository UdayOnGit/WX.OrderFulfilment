using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public class GetRecommendedProducts : IGetProducts
    {
        private readonly IGetWooliesProducts _getWooliesProducts;
        
        public GetRecommendedProducts(IGetWooliesProducts getWooliesProducts)
        {
            _getWooliesProducts = getWooliesProducts;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
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
    }
}