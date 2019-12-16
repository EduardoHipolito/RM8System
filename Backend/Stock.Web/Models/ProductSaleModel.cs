using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stock.Web.Models
{
    public class ProductSaleModel : BaseModel
    {
        public int IdProduct { get; set; }
        public ProductModel FKProduct { get; set; }

        public int Quantity { get; set; }
    }
}
