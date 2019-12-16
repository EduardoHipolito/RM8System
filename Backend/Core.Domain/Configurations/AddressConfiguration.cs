
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Domain.Configurations
{
    public class AddressConfiguration
    {
        public AddressConfiguration(EntityTypeBuilder<Address> entity)
        {

        }
    }
}
