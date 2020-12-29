using AutoMapper;
using Operation.Model;
using View.Model;

namespace Model.Mapper.OperationToView
{
    sealed class OrderDetailMapToOrderDetailInfo : Profile
    {
        public OrderDetailMapToOrderDetailInfo()
        {
            this.CreateMap<OrderDetail, OrderDetailInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Product, opt => opt.MapFrom((src, dest, obj, context) => src.Product != null ? context.Mapper.Map<ProductInfo>(src.Product) : null))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}