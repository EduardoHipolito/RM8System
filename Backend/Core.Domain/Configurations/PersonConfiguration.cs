using Core.Domain.Enum;
using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core.Domain.Configurations
{
    public class PersonConfiguration
    {
        public PersonConfiguration(EntityTypeBuilder<Person> entity)
        {
            entity.HasDiscriminator<int>("Type")
            .HasValue<Person>(0)
            .HasValue<PhysicalPerson>(1)
            .HasValue<LegalPerson>(2);
        }
    }
}

