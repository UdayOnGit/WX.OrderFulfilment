using System.Collections.Generic;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Services;

namespace WX.OrderFulfilment.Services
{
	public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(SortOptionEnum sortOption);
    }
}
