using Framework.Domain;
using Stock.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Domain
{
    public class Stock : EntityBase
    {
        [ForeignKey(nameof(FKProduct))]
        public int IdProduct { get; set; }
        public Product FKProduct { get; set; }

        public StockType StockType { get; set; }
        public StockHitType? StockHitType { get; set; }

        [ForeignKey(nameof(FKSale))]
        public int? IdSale { get; set; }
        public Sale FKSale { get; set; }

        [ForeignKey(nameof(FKEntry))]
        public int? IdEntry { get; set; }
        public Entry FKEntry { get; set; }
        public decimal Quantity { get; set; }
    }
}
