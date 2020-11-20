using Microsoft.Extensions.Configuration;
using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Repository
{
	public class OrderDetailsRepository : IRepository
    {
        private readonly IConfiguration _configuration;

        public OrderDetailsRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserDetails GetUserDetails()
        {
            var name = _configuration.GetValue<string>("UserDetails:Name");
            var token = _configuration.GetValue<string>("UserDetails:Token");

            return new UserDetails() { Name = name, Token = token };
        }
    }
}
