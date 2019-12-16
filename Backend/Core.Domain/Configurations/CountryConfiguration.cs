using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.Configurations
{
    public class CountryConfiguration
    {
        public CountryConfiguration(EntityTypeBuilder<Country> entity)
        {
            entity.HasIndex(x => x.Code).IsUnique().HasName("IX_UniqueCountry");

        }
    }
}
