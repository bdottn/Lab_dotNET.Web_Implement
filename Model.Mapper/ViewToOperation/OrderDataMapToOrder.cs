using AutoMapper;
using Operation.Model;
using System.Collections.Generic;
using View.Model;

namespace Model.Mapper.ViewToOperation
{
    sealed class OrderDataMapToOrder : Profile
    {
        public OrderDataMapToOrder()
        {
            this.CreateMap<OrderData, Order>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Customer, opt => opt.Ignore())
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
                .ForMember(dest => dest.CreatedTime, opt => opt.Ignore())
                .ForMember(dest => dest.LatestModifiedTime, opt => opt.Ignore())
                .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom((src, dest, obj, context) => src.OrderDetails != null ? context.Mapper.Map<List<OrderDetail>>(src.OrderDetails) : null));
        }
    }
}