﻿using Core.Domain.Enum;
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
    public class PhoneConfiguration
    {
        public PhoneConfiguration(EntityTypeBuilder<Phone> entity)
        {
        }
    }
}

