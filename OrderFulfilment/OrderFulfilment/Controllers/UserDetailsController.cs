using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Resources;
using WX.OrderFulfilment.Services;

namespace WX.OrderFulfilment.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserDetailsController : ControllerBase
    {
        readonly IUserDetailsService _userDetailsService;
        private readonly IMapper _mapper;

        public UserDetailsController(IUserDetailsService userDetailsService, IMapper mapper)
        {
            _userDetailsService = userDetailsService;
            _mapper = mapper;
        }

        [HttpGet("user")]
        public UserDetailsResource GetUserDetails()
        {
            // Assumptions: This api endpoint will always return single user. If we are to return a list of users, it should be fairly simple to do so.
            var userDetails = _userDetailsService.GetUserDetails();

            if (userDetails == null)
            {
                // Log message?
            }

            var resource = _mapper.Map<UserDetails, UserDetailsResource>(userDetails);

            return resource;
        }

        //[HttpGet("sort")]
        //public IActionResult GetProducts([FromQuery(Name = "sortOption")] string sortOption)
        //{
        //    if (string.IsNullOrEmpty(sortOption))
        //    {
        //        return BadRequest();
        //    }

        //    //var products = _
        //    //var resource = _mapper.Map<Product, ProductResource>()

        //    return Ok();
        //}

    }
}
