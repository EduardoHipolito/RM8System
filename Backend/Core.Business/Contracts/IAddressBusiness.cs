
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Framework.Business.Factory;
using Framework.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Contracts
{
    [FactoryReference("Core.Business.Commands.AddressBusiness, Core.Business")]
    public interface IAddressBusiness : IBusinessBase<Address>
    {

    }
}
