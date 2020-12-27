using AutoMapper;
using Operation.Model;
using View.Model;

namespace Model.Mapper.OperationToView
{
    sealed class CustomerMapToCustomerInfo : Profile
    {
        public CustomerMapToCustomerInfo()
        {
            this.CreateMap<Customer, CustomerInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LatestModifiedTime, opt => opt.MapFrom(src => src.LatestModifiedTime));
        }
    }
}