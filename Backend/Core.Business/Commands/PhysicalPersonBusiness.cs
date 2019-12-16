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
    public class PhysicalPersonBusiness : BusinessBase<PhysicalPerson, PhysicalPersonDataAccess>, IPhysicalPersonBusiness
    {
        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.PhysicalPerson);
        }
        public override bool DeleteAll(RequestBase<List<PhysicalPerson>> request)
        {
            if (request.Parameter == null)
            {
                throw new BusinessException("Preencha os dados obrigatórios");
            }
            var Remove = new List<PhysicalPerson>();
            foreach (var item in request.Parameter)
            {
                var userDataAccess = FactoryDataAccess<User, UserDataAccess>();
                if (!userDataAccess.GetAll().Any(z => z.IdPerson == item.Id))
                {
                    Remove.Add(item);
                }
            }
            var deleteRequest = new RequestBase<List<PhysicalPerson>>();
            deleteRequest.UserId = request.UserId;
            deleteRequest.IdCompany = request.IdCompany;
            deleteRequest.Parameter = Remove;

            return base.DeleteAll(deleteRequest);
        }
    }
}