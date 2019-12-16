
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
    [FactoryReference("Core.Business.Commands.CompanyBusiness, Core.Business")]
    public interface ICompanyBusiness : IBusinessBase<Company>
    {
        IList<Company> CompanyByUsers(RequestBase request);
        IList<Company> GetAllMasters(RequestBase requestBase);
    }
}
