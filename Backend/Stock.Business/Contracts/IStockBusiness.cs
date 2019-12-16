using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using Stock.Business.Entities;
using Stock.Business.Message.Requests;
using Stock.Domain;
using Stock.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Contracts
{
    [FactoryReference("Stock.Business.Commands.StockBusiness, Stock.Business")]
    public interface IStockBusiness : IBusinessBase<Stock.Domain.Stock>
    {
        List<Stock.Domain.Stock> CreateStock(CreateStockByEntryRequest request);
        List<Stock.Domain.Stock> CreateStock(CreateStockBySaleRequest request);
        Domain.Stock Hit(HitStockRequest request);
        IList<StockSum> GetSummary(RequestBase request);
        IList<StockHistory> GetHistoryByIdProduct(RequestById request);
    }
}
