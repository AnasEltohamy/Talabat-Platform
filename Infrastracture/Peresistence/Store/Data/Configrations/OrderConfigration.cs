using DomainLayer.Models.OrderModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Peresistence.Store.Data.Configrations
{
    internal class OrderConfigration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Order");
            builder.HasMany(Or => Or.Items).WithOne();
            builder.HasOne(Or=>Or.DeliveryMethod).WithMany().HasForeignKey(Or => Or.DeliveryMethodId);
            builder.Property(Or => Or.SubTotal).HasColumnType("decimal(8,2)");
            builder.OwnsOne(Or => Or.Address);
        }
    }
}
