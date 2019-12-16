using Framework.Business.Request;
using Stock.Domain;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Message.Requests
{
    public class CreateStockByEntryRequest : RequestBase
    {
        public Entry Entry { get; set; }
        public StockType StockType { get; set; }
    }
}
