using Framework.Business.Request;
using Stock.Domain;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Message.Requests
{
    public class HitStockRequest : RequestBase
    {
        public Product Product { get; set; }
        public StockHitType StockHitType { get; set; }
        public decimal Quantity { get; set; }
    }
}
