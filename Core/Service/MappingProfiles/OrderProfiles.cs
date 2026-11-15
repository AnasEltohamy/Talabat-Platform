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
                .ForMember(D=>D.DeliveryMethod , O=>O.MapFrom(S=>S.DeliveryMethod.ShortName ));

            CreateMap<OrderItem, OrderItemDTO>()
                .ForMember(D => D.ProductName, O => O.MapFrom(S => S.Product.ProductName))
                .ForMember(D => D.ProductId, O => O.MapFrom(S => S.Product.ProductId))
                .ForMember(D => D.PictureUrl, O => O.MapFrom<OrderPictureUrlValueResolver>());
            CreateMap<DeliveryMethod, DeliveryMethodDTO>();
        }
    }
}
