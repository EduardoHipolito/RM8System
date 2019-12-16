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
    [FactoryReference("Stock.Business.Commands.EntryBusiness, Stock.Business")]
    public interface IEntryBusiness : IBusinessBase<Entry>
    {
        bool CancelEntry(RequestBase<Entry> request);
        SumByQuantity GetDailyEntries(RequestBase request);
        SumByQuantity GetMonthEntries(RequestBase request);
    }
}
