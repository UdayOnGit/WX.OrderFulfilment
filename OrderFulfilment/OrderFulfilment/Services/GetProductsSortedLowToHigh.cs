using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public class GetProductsSortedLowToHigh : IGetProducts
    {
        private readonly IGetWooliesProducts _getWooliesProducts;
        
        public GetProductsSortedLowToHigh(IGetWooliesProducts getWooliesProducts)
        {
            _getWooliesProducts = getWooliesProducts;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            var products = await _getWooliesProducts.GetProductsFromWXAPI();
			return products.OrderBy(product => product.Price);
        }
    }
}