using AutoMapper;
using DomainLayer.Models.ProductModels;
using Shared.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class ProductProfile:Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>()
                .ForMember(Dist => Dist.Brand, Opt => Opt.MapFrom(Sor=>Sor.ProductBrand.Name))
                .ForMember(Dist=>Dist.Type,Opt=>Opt.MapFrom(Sor=>Sor.ProductType.Name))
                .ForMember(Dist => Dist.PictureUrl, Opt =>
                {
                    Opt.MapFrom<PictureUrlValueResolver>();
                });

            CreateMap<ProductType ,TypeDTO>();
            CreateMap<ProductBrand ,BrandDTO>();
        }
    }
}
