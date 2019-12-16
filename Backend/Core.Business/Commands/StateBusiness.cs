using Core.Business.Contracts;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Framework.Business;
using Framework.Business.Factory;
using Framework.Business.Request;
using Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Business.Commands
{
    public class StateBusiness : BusinessBase<State, StateDataAccess>, IStateBusiness
    {
        public IList<State> FindByCountry(RequestById request)
        {
            return this._dataAccess.GetAll(IsFullUser(request.UserId ) ? 0 : request.IdCompany ).Where(w => w.IdCountry == request.Id).ToList();
        }
        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.State);
        }
    }
}