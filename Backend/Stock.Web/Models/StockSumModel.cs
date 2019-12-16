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
    public class StockSumModel
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
