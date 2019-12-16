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
    public class StockHistoryModel
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
