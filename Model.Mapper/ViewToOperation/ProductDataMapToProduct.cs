using AutoMapper;
using Operation.Model;
using View.Model;

namespace Model.Mapper.ViewToOperation
{
    sealed class ProductDataMapToProduct : Profile
    {
        public ProductDataMapToProduct()
        {
            this.CreateMap<ProductData, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.CreatedTime, opt => opt.Ignore())
                .ForMember(dest => dest.LatestModifiedTime, opt => opt.Ignore());
        }
    }
}