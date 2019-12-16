using Core.Business.Contracts;
using Core.Business.Message.Requests;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Framework.Business;
using Framework.Business.Exceptions;
using Framework.Business.Factory;
using Framework.Business.Request;
using Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Business.Commands
{
    public class UserAplicationCompanyBusiness : BusinessBase<UserAplicationCompany, UserAplicationCompanyDataAccess>, IUserAplicationCompanyBusiness
    {
        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return this.ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.UserAplicationCompany);
        }

        public UserAplicationCompanyDataAccess dataAccess
        {
            get
            {
                return _dataAccess as UserAplicationCompanyDataAccess;
            }
        }

        public IList<string> GetAllRoles(RequestBase request)
        {
            return dataAccess.GetAll().Where(w => w.IdUser == request.UserId).Select(s => s.FKAplication.AplicationCode).ToList();
        }

        public IList<Module> GetMenuApplication(RequestBase request)
        {
            var rules = dataAccess.GetAll().Where(w => w.IdUser == request.UserId && w.IdCompany == request.IdCompany).ToList();

            var moduleDataAccess = FactoryDataAccess<Module, ModuleDataAccess>();
            var aplicationDataAccess = FactoryDataAccess<Aplication, AplicationDataAccess>();
            var modules = moduleDataAccess.GetAll(request.IdCompany);
            modules = aplicationDataAccess.Include(modules);

            var moduleList = modules.ToList();

            foreach (var module in moduleList)
            {
                module.Aplications = module.Aplications.Where(w => w.ShowMenu && rules.Select(s => s.IdAplication).Contains(w.Id)).ToList();
            }
            moduleList = moduleList.Where(w => w.Aplications.Any()).ToList();

            return moduleList;
        }

        public override bool Add(RequestBase<UserAplicationCompany> request)
        {
            if (GetAll(request).Any(x => x.IdAplication == request.Parameter.IdAplication && x.IdUser == request.Parameter.IdUser && x.IdCompany == request.Parameter.IdCompany))
            {
                throw new BusinessException("Aplicação ja existente");
            }
            return base.Add(request);
        }

        public bool ValidatePermition(int IdUser, int IdCompany, string ModuleCode, string AplicationCode)
        {
            return dataAccess.GetAll(0).Any(a => a.IdUser == IdUser && a.IdCompany == IdCompany && a.FKAplication.AplicationCode == AplicationCode && a.FKAplication.FKModule.ModuleCode == ModuleCode);
        }

        public bool AuthorizeAplication(AuthorizeAplicationRequest request)
        {
            return dataAccess.GetAll(0).Any(a => a.IsGlobal || (request.OperationType == "READ" || a.AccessLevel == Domain.Enum.AccessLevel.Full) && a.IdUser == request.UserId && (a.IdCompany == request.IdCompany || request.IdCompany == 0) && a.FKAplication.AplicationCode == request.AplicationCode && a.FKAplication.FKModule.ModuleCode == request.ModuleCode);
        }
    }
}