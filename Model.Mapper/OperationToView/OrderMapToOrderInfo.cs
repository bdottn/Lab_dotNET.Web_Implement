using AutoMapper;
using Operation.Model;
using System.Collections.Generic;
using View.Model;

namespace Model.Mapper.OperationToView
{
    sealed class OrderMapToOrderInfo : Profile
    {
        public OrderMapToOrderInfo()
        {
            this.CreateMap<Order, OrderInfo>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Customer, opt => opt.MapFrom((src, dest, obj, context) => src.Customer != null ? context.Mapper.Map<CustomerInfo>(src.Customer) : null))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CreatedTime, opt => opt.MapFrom(src => src.CreatedTime))
                .ForMember(dest => dest.LatestModifiedTime, opt => opt.MapFrom(src => src.LatestModifiedTime))
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom((src, dest, obj, context) => src.OrderDetails != null ? context.Mapper.Map<List<OrderDetailInfo>>(src.OrderDetails) : null));
        }
    }
}