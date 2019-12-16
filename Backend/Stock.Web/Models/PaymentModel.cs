using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class PaymentModel : BaseModel
    {
        public int IdFormOfPayment { get; set; }
        public FormOfPaymentModel FKFormOfPayment { get; set; }

        public int IdSale { get; set; }
        public SaleModel FKSale { get; set; }

        public string NSU { get; set; }

        public decimal Value { get; set; }

        public int NumberOfInstallments { get; set; }

        public decimal FormOfPaymentRate { get; set; }
    }
}
