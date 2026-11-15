using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModuls;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles.Helper
{
    public class OrderPictureUrlValueResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDTO, string>
    {
        public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            return $"{_configuration.GetSection("URLs")["baseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
