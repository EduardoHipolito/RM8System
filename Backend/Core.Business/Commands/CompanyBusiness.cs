using Core.Business.Contracts;
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
    public class CompanyBusiness : BusinessBase<Company, CompanyDataAccess>, ICompanyBusiness
    {
        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Company);
        }

        public IList<Company> CompanyByUsers(RequestBase request)
        {
            var query = this._dataAccess.CompaniesByUser(request.UserId );

            if (!query.Any())
            {
                throw new BusinessException("Nenhuma loja vinculada ao usuario selecionado");
            }

            return query.ToList();
        }

        public override bool DeleteAll(RequestBase<List<Company>> request)
        {
            if (request.Parameter == null)
            {
                throw new BusinessException("Preencha os dados obrigatórios");
            }
            var Remove = new List<Company>();
            foreach (var item in request.Parameter)
            {
                var userAplicationCompanyDataAccess = FactoryDataAccess<UserAplicationCompany, UserAplicationCompanyDataAccess>();
                if (!userAplicationCompanyDataAccess.GetAll().Any(z => z.IdCompany == item.Id))
                {
                    Remove.Add(item);
                }
            }
            var deleteRequest = new RequestBase<List<Company>>();
            deleteRequest.UserId = request.UserId;
            deleteRequest.IdCompany = request.IdCompany;
            deleteRequest.Parameter = Remove;
            return base.DeleteAll(deleteRequest);
        }
        public override bool Add(RequestBase<Company> request)
        {
            if (request.Parameter.IdMaster < 1)
            {
                request.Parameter.IdMaster = null;
            }
            if (GetAll(request).Any(x => x.IdPerson == request.Parameter.IdPerson) && request.Parameter.IdMaster == null)
            {
                throw new BusinessException("Empresa ja existente");
            }
            if (request.Parameter.Type == Domain.Enum.CompanyType.Child && request.Parameter.IdMaster == null)
            {
                throw new BusinessException("Uma empresa filial precisa de uma matriz");
            }
            if (request.Parameter.Type == Domain.Enum.CompanyType.Master && request.Parameter.IdMaster != null)
            {
                throw new BusinessException("Uma empresa matriz não pode ter uma filial");
            }
            if (request.Parameter.IdMaster != null)
            {
                if (!GetAll(request).Any(x => x.Id == request.Parameter.IdMaster))
                {
                    throw new BusinessException("Matriz inexistente");
                }
            }
            return base.Add(request);
        }

        public IList<Company> GetAllMasters(RequestBase request)
        {
            return _dataAccess.GetAll(IsFullUser(request.UserId ) ? 0 : request.IdCompany ).Where(w => w.Type == Domain.Enum.CompanyType.Master).ToList();
        }
    }
}