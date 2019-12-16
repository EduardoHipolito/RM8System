using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.Configurations
{
    public class ModuleConfiguration
    {
        public ModuleConfiguration(EntityTypeBuilder<Module> entity)
        {
            entity.HasIndex(x => new { x.Name, x.Description}).IsUnique().HasName("IX_UniqueModule");

        }
    }
}

