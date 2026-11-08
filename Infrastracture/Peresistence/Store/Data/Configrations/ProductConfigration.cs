using DomainLayer.Models.ProductModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistence.Store.Data.Configrations
{
    internal class ProductConfigration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(P=>P.ProductBrand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(P=>P.ProductType).WithMany().HasForeignKey(p => p.TypeId);
        }
    }
}
