using AutoMapper;
using Operation.Model;
using View.Model;

namespace Model.Mapper.ViewToOperation
{
    sealed class OrderDetailDataMapToOrderDetail : Profile
    {
        public OrderDetailDataMapToOrderDetail()
        {
            this.CreateMap<OrderDetailData, OrderDetail>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.OrderId, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Product, opt => opt.Ignore())
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
        }
    }
}