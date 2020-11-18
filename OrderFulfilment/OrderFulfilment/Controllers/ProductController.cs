﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Resources;
using WX.OrderFulfilment.Services;

namespace WX.OrderFulfilment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet()]
        public IEnumerable<ProductResource> GetProducts([FromQuery(Name = "sortOption")] string sortOption)
        {
            // ToDo: Send bad request
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest("missing sort option!");
            //}

            var products = _productService.GetProducts(sortOption);

            var resource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return resource;
        }
    }
}
