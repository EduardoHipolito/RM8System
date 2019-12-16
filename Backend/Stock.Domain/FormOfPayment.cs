using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class FormOfPayment : EntityBase
    {
        public string Name { get; set; }

        public int MinNumberOfInstallments { get; set; }

        public int MaxNumberOfInstallments { get; set; }

        public decimal MinimumValue { get; set; }

        public string MoreInformation { get; set; }

        public decimal Rate { get; set; }
    }
}
