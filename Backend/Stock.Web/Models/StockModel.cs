using Core.Domain.Enum;
using Framework.Domain;
using Framework.Web.Models;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Stock.Web.Models
{
    public class StockModel : BaseModel
    {
        public int IdProduct { get; set; }
        public ProductModel FKProduct { get; set; }

        public StockType StockType { get; set; }
        public StockHitType? StockHitType { get; set; }

        public int? IdSale { get; set; }
        public SaleModel FKSale { get; set; }

        [ForeignKey(nameof(FKEntry))]
        public int? IdEntry { get; set; }
        public EntryModel FKEntry { get; set; }
        public decimal Quantity { get; set; }
    }
}
