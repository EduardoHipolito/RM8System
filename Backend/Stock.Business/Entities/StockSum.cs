using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Entities
{
    public class StockSum
    {
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public string ProductName { get; set; }
        public string Brand { get; set; }
        public string Picture { get; set; }
        public string InternalNumber { get; set; }
        public string BarCode { get; set; }
        public decimal Quantity { get; set; }
        public DateTime CreateDate { get; internal set; }
    }
}
