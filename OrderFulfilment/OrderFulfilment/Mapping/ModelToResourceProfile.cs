using AutoMapper;
using WX.OrderFulfilment.Model;
using WX.OrderFulfilment.Resources;

namespace WX.OrderFulfilment.Mapping
{
	public class ModelToResourceProfile : Profile
    {
        public ModelToResourceProfile()
        {
            CreateMap<UserDetails, UserDetailsResource>();
            CreateMap<Product, ProductResource>();
        }
    }
}
