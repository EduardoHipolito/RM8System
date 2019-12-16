using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain.Configurations
{
   public class StateConfiguration
    {
        public StateConfiguration(EntityTypeBuilder<State> entity)
        {
            entity.HasIndex(x => new { x.Name, x.IdCountry }).IsUnique().HasName("IX_UniqueEstado");
        }
    }
}

