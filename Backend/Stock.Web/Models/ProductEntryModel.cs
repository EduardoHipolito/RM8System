using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class ProductEntryModel : BaseModel
    {
        public int IdProduct { get; set; }
        public ProductModel FKProduct { get; set; }

        public int? IdEntry { get; set; }
        public EntryModel FKEntry { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal ICMS { get; set; }

        public decimal IPI { get; set; }

        public decimal Quantity { get; set; }

    }
}
