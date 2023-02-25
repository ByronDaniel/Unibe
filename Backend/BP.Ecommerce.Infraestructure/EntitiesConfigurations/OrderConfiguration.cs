using BP.Ecommerce.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Ecommerce.Infraestructure.EntitiesConfigurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

            builder.Property<DateTime>("DateCreation")
                .HasColumnType("datetime2");

            builder.Property<DateTime>("DateDeleted")
                .HasColumnType("datetime2");

            builder.Property<DateTime>("DateModification")
                .HasColumnType("datetime2");

            builder.Property<Guid?>("DeliveryMethodId")
                .HasColumnType("uniqueidentifier");

            builder.Property<string>("State")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property<decimal>("Subtotal")
                .HasColumnType("decimal(18,2)");

            builder.Property<decimal>("TotalPrice")
                .HasColumnType("decimal(18,2)");

            builder.HasKey("Id");

            builder.HasIndex("DeliveryMethodId");

            builder.ToTable("Order");

            builder.HasOne("BP.Ecommerce.Domain.Entities.DeliveryMethod", "DeliveryMethod")
                        .WithMany()
                        .HasForeignKey("DeliveryMethodId");

            builder.Navigation("DeliveryMethod");
            builder.Navigation("orderProducts");
        }
    }
}
