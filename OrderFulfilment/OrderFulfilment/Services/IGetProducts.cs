using System.Collections.Generic;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public interface IGetProducts
    {
        Task<IEnumerable<Product>> GetProductsAsync();
    }
}