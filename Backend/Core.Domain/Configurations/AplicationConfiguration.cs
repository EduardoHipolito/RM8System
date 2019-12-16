using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Globalization;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Core.Domain.Configurations
{
    public class AplicationConfiguration
    {
        public AplicationConfiguration(EntityTypeBuilder<Aplication> entity)
        {

            entity.HasIndex(x => new { x.Name, x.Description }).IsUnique().HasName("IX_UniqueAplication");

            entity.HasIndex(x => new { x.IdModule, x.Index }).IsUnique().HasName("IX_UniqueAplicationModuleIndex");
        }
    }
}
