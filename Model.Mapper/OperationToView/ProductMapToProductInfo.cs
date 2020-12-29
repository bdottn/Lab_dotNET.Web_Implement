using AutoMapper;
using Operation.Model;
using View.Model;

namespace Model.Mapper.OperationToView
{
    sealed class ProductMapToProductInfo : Profile
    {
        public ProductMapToProductInfo()
        {
            this.CreateMap<Product, ProductInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LatestModifiedTime, opt => opt.MapFrom(src => src.LatestModifiedTime));
        }
    }
}