using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P129NLayerArchitectura.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace P129NLayerArchitectura.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(b => b.Title).HasMaxLength(255).IsRequired(true);
        }
    }
}
