using AutoMapper;
using DomainLayer.Models.IdentityModuls;
using DomainLayer.Models.OrderModel;
using DomainLayer.Models.OrderModuls;
using Service.MappingProfiles.Helper;
using Shared.DTOs.Identity;
using Shared.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfiles:Profile
    {
        public OrderProfiles()
        {
            CreateMap<AddressDTO, OrderAddress>().ReverseMap();
            CreateMap<Order, OrderToReturnDTO>()
                .ForMember(d => d.BuyerEmail, o => o.MapFrom(s => s.UserEmail))
                .ForMember(d => d.ShipToAddress, o => o.MapFrom(s => s.Address))
                .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
                .ForMember(d => d.DeliveryCost, o => o.MapFrom(s => s.DeliveryMethod.Cost))
                .ForMember(d => d.Subtotal, o => o.MapFrom(s => s.SubTotal))
                .ForMember(d => d.Status, o => o.MapFrom(s => s.OrderStatus.ToString()));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderPictureUrlValueResolver>());
            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
        }
    }
}
