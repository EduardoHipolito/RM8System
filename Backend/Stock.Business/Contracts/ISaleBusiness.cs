using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using Stock.Business.Entities;
using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Business.Contracts
{
    [FactoryReference("Stock.Business.Commands.SaleBusiness, Stock.Business")]
    public interface ISaleBusiness : IBusinessBase<Sale>
    {
        Task<SumByQuantity> GetDailySalesAmout(RequestBase request);
        Task<SumByQuantity> GetMonthSalesAmout(RequestBase request);
        Task<SumByQuantity> GetDailyProfit(RequestBase request);
        Task<SumByQuantity> GetMonthProfit(RequestBase request);
    }
}
