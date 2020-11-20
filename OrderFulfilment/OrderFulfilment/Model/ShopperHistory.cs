using System.Collections.Generic;

namespace WX.OrderFulfilment.Model
{
	public class ShopperHistory
	{
		public int CustomerId { get; set; }
		public List<Product> Products { get; set; }
	}
}
