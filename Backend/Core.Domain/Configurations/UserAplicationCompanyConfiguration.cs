using Core.Domain.Enum;
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
    public class UserAplicationCompanyConfiguration
    {
        public UserAplicationCompanyConfiguration(EntityTypeBuilder<UserAplicationCompany> entity)
        {
            entity.HasIndex(x => new { x.IdAplication, x.IdCompany, x.IdUser, }).IsUnique().HasName("IX_UniquePermition");
        }
    }
}

