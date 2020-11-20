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
            var userDetails = _userDetailsService.GetUserDetails();

            if (userDetails == null)
            {
                // Have logger log a message
            }

            var resource = _mapper.Map<UserDetails, UserDetailsResource>(userDetails);

            return resource;
        }
    }
}
