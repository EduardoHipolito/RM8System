using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class SaleModel : BaseModel
    {
        public int? IdCustomer { get; set; }
        public CustomerModel FKCustomer { get; set; }

        public decimal Discount { get; set; } = 0;

        public decimal Shipping { get; set; }

        public IList<ProductSaleModel> Products { get; set; }
        public IList<PaymentModel> Payments { get; set; }
    }
}
