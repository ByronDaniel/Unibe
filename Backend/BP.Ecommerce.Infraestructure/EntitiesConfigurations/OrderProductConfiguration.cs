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
    public class OrderProductConfiguration : IEntityTypeConfiguration<OrderProduct>
    {
        public void Configure(EntityTypeBuilder<OrderProduct> builder)
        {
            builder.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

            builder.Property<DateTime>("DateCreation")
                .HasColumnType("datetime2");

            builder.Property<DateTime>("DateModification")
                .HasColumnType("datetime2");

            builder.Property<Guid>("OrderId")
                .HasColumnType("uniqueidentifier");

            builder.Property<Guid>("ProductId")
                .HasColumnType("uniqueidentifier");

            builder.Property<int>("ProductQuantity")
                .HasColumnType("int");

            builder.Property<decimal>("Total")
                .HasColumnType("decimal(18,2)");

            builder.HasKey("Id");

            builder.HasIndex("OrderId");

            builder.HasIndex("ProductId");

            builder.ToTable("OrderProducts");

            builder.HasOne("BP.Ecommerce.Domain.Entities.Order", "Order")
                        .WithMany("orderProducts")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

            builder.HasOne("BP.Ecommerce.Domain.Entities.Product", "Product")
                .WithMany()
                .HasForeignKey("ProductId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Navigation("Order");

            builder.Navigation("Product");

        }
    }
}
