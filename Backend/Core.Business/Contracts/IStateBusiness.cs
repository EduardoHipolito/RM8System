
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Business.Contracts
{
    [FactoryReference("Core.Business.Commands.StateBusiness, Core.Business")]
    public interface IStateBusiness : IBusinessBase<State>
    {
        IList<State> FindByCountry(RequestById request);
    }
}
