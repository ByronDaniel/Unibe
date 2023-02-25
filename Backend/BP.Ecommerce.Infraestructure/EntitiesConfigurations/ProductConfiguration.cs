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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

            builder.Property<Guid>("BrandId")
                .HasColumnType("uniqueidentifier");

            builder.Property<DateTime>("DateCreation")
                .HasColumnType("datetime2");

            builder.Property<DateTime>("DateDeleted")
                .HasColumnType("datetime2");

            builder.Property<DateTime>("DateModification")
                .HasColumnType("datetime2");

            builder.Property<string>("Description")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(40)
                .HasColumnType("nvarchar(40)");

            builder.Property<decimal>("Price")
                .HasColumnType("decimal(18,2)");

            builder.Property<Guid>("ProductTypeId")
                .HasColumnType("uniqueidentifier");

            builder.Property<string>("State")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.Property<int>("Stock")
                .HasColumnType("int");

            builder.HasKey("Id");

            builder.HasIndex("BrandId");

            builder.HasIndex("ProductTypeId");

            builder.ToTable("Products", (string)null);

            builder.HasOne("BP.Ecommerce.Domain.Entities.Brand", "Brand")
                        .WithMany()
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

            builder.HasOne("BP.Ecommerce.Domain.Entities.ProductType", "ProductType")
                .WithMany()
                .HasForeignKey("ProductTypeId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder.Navigation("Brand");

            builder.Navigation("ProductType");
        }
    }
}
