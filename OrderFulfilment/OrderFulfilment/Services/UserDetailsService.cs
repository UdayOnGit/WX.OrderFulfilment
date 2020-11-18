using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Repository;

namespace WX.OrderFulfilment.Services
{
    public class UserDetailsService : IUserDetailsService
    {
        private readonly IRepository _repository;

        public UserDetailsService(IRepository repository)
        {
            _repository = repository;
        }

        public UserDetails GetUserDetails()
        {
            return _repository.GetUserDetails();
        }
    }
}
