﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WX.OrderFulfilment.Model
{
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public long Quantity { get; set; }
    }
}