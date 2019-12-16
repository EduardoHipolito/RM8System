using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Stock.Domain.Configurations
{
    public class ProductConfiguration
    {
        public ProductConfiguration(EntityTypeBuilder<Product> entity)
        {
            entity.HasIndex(x => new { x.Name, x.Color }).IsUnique().HasName("IX_UniqueProduct");
        }
    }
}
