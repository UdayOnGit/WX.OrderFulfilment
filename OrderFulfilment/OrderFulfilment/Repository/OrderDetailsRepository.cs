using WX.OrderFulfilment.Model;

namespace WX.OrderFulfilment.Repository
{
    public class OrderDetailsRepository : IRepository
    {
        public UserDetails GetUserDetails()
        {
            // Ideally this information will come different source like a DB or different service
            const string name = "Uday";
            const string token = "1234-455662-22233333-3333";

            return new UserDetails() { Name = name, Token = token };
        }
    }
}
