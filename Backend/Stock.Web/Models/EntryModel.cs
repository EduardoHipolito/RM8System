using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class EntryModel : BaseModel
    {

        public int? IdSupplier { get; set; }
        public SupplierModel FKSupplier { get; set; }

        public decimal Discount { get; set; } = 0;

        [Required(ErrorMessage = "O preço do frete é obrigatório")]
        public decimal Shipping { get; set; }

        public List<ProductEntryModel> ProductEntries { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
