using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Repository
{
    public class OrderDetailsRepository : IRepository
    {
        public UserDetails GetUserDetails()
        {
            // Ideally this information will come different source like a DB or different service
            const string name = "Uday Jadhav";
            const string token = "276b32a9-8e35-4981-87b4-85e0a30f3319";

            return new UserDetails() { Name = name, Token = token };
        }
    }
}
