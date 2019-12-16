using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Stock.Domain;
using System.Collections.Generic;

namespace Stock.Business.Contracts
{
    [FactoryReference("Stock.Business.Commands.ProductionBusiness, Stock.Business")]
    public interface IProductionBusiness : IBusinessBase<Production>
    {

    }
}
