using System.Collections.Generic;
using System.Threading.Tasks;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
	public interface IProductService
    {
        Task<IEnumerable<Product>> GetProducts(string sortOption);
    }
}
