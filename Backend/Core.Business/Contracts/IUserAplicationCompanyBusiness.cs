
using Core.Business.Message.Requests;
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
    [FactoryReference("Core.Business.Commands.UserAplicationCompanyBusiness, Core.Business")]
    public interface IUserAplicationCompanyBusiness : IBusinessBase<UserAplicationCompany>
    {
        bool ValidatePermition(int IdUser, int IdCompany, string ModuleCode, string AplicationCode);
        bool AuthorizeAplication(AuthorizeAplicationRequest request);
        IList<Module> GetMenuApplication(RequestBase request);
        IList<string> GetAllRoles(RequestBase request);
    }
} 
