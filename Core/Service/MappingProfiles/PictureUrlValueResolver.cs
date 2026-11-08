using AutoMapper;
using DomainLayer.Models.ProductModels;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    internal class PictureUrlValueResolver(IConfiguration configuration) : IValueResolver<Product, ProductDTO, string>
    {
        public string Resolve(Product source, ProductDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
            {
                return string.Empty;
            }
            else
            {
                //return $"{configuration.GetSection("URLs")["baseUrl"]}{source.PictureUrl}";
                return $"{configuration.GetSection("URLs")["baseUrl"]}{source.PictureUrl}";
            }
        }
    }
}
