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
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
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

            builder.Property<string>("Name")
                .IsRequired()
                .HasMaxLength(30)
                .HasColumnType("nvarchar(30)");

            builder.Property<string>("State")
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasKey("Id");

            builder.ToTable("ProductTypes", (string)null);
        }
    }
}
