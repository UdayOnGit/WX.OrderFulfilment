using System.Collections.Generic;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public interface IGetWooliesProducts
    {
        Task<IEnumerable<Product>> GetProductsFromWXAPI();
        Task<IEnumerable<ShopperHistory>> GetShopperHistory();
    }
}