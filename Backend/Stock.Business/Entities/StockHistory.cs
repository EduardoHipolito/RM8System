using Stock.Domain;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Entities
{
    public class StockHistory
    {
        public string ProductName { get; set; }
        public StockType StockType { get; set; }
        public StockHitType? StockHitType { get; set; }
        public decimal Quantity { get; set; }
        public string SupplierName { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreateDate { get; set; }
        public int IdProduct { get; set; }
    }
}
