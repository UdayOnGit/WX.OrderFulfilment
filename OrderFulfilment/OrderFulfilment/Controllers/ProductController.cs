using System;
using System.Collections.Generic;
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

        [HttpGet("sort")]
        public async Task<IActionResult> GetProducts([FromQuery(Name = "sortOption")] string sortOption)
        {
            if(!Enum.TryParse(typeof(SortOptionEnum), sortOption, true, out var optionEnum))
            {
                return BadRequest($"Invalid sortoption: {sortOption}");
            }

            var products = await _productService.GetProducts((SortOptionEnum)optionEnum); // ToDo: This is absurd, why do I have to convert again

            var resource = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductResource>>(products);

            return Ok(resource);
        }
    }
}
