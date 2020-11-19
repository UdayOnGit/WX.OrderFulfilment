using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.OrderFulfilment.Model
{
	public class ShopperHistory
	{
		public int CustomerId { get; set; }
		public List<Product> Products { get; set; }
	}
}
