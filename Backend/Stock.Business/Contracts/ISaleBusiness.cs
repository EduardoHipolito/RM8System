using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using Stock.Business.Entities;
using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Contracts
{
    [FactoryReference("Stock.Business.Commands.SaleBusiness, Stock.Business")]
    public interface ISaleBusiness : IBusinessBase<Sale>
    {
        SumByQuantity GetDailySalesAmout(RequestBase request);
        SumByQuantity GetMonthSalesAmout(RequestBase request);
        SumByQuantity GetDailyProfit(RequestBase request);
        SumByQuantity GetMonthProfit(RequestBase request);
    }
}
