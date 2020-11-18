using System.Collections.Generic;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Services
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts(string sortOption);
    }
}
