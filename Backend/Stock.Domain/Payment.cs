using Framework.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Domain
{
    public class Payment : EntityBase
    {
        [ForeignKey(nameof(FKFormOfPayment))]
        public int IdFormOfPayment { get; set; }
        public FormOfPayment FKFormOfPayment { get; set; }

        [ForeignKey(nameof(FKSale))]
        public int IdSale { get; set; }
        public Sale FKSale { get; set; }

        public string NSU { get; set; }

        public decimal Value { get; set; }

        public int NumberOfInstallments { get; set; }

        public decimal FormOfPaymentRate { get; set; }
    }
}
