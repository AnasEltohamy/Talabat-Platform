using AutoMapper;
using DomainLayer.Models.BasketModels;
using Shared.DTOs.Basket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class BasketProfile:Profile
    {
        public BasketProfile()
        {
            CreateMap<CustomerBasket ,CustomerBasketDTO>().ReverseMap();
            CreateMap<BasketItems ,BasketItemsDTO>().ReverseMap();
        }
    }
}
