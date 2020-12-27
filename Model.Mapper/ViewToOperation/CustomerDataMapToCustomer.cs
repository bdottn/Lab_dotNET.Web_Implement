using AutoMapper;
using Operation.Model;
using View.Model;

namespace Model.Mapper.ViewToOperation
{
    sealed class CustomerDataMapToCustomer : Profile
    {
        public CustomerDataMapToCustomer()
        {
            this.CreateMap<CustomerData, Customer>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.CreatedTime, opt => opt.Ignore())
                .ForMember(dest => dest.LatestModifiedTime, opt => opt.Ignore());
        }
    }
}