using Framework.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain.Configurations
{
    public class FormOfPaymentConfiguration
    {
        public FormOfPaymentConfiguration(EntityTypeBuilder<FormOfPayment> entity)
        {
        }
    }
}
