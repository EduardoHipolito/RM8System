
using Core.Business.Entities;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Core.Domain.Enum;
using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Contracts
{
    [FactoryReference("Core.Business.Commands.UserBusiness, Core.Business")]
    public interface IUserBusiness : IBusinessBase<User>
    {
        bool Login(ref int? IdUser, string login, string pass);

        bool ChangePassword(RequestBase<UserChangePassword> request);

        bool ChangePasswordByToken(RequestBase<UserChangePassword> request);

        Guid GetNewPassHash(RequestBase<string> request);

        void LoginAlreadyRegistered(RequestBase<string> request);

        User Get(RequestBase<string> request);

        User GetByIdPerson(RequestBase<int> request);

        ProfileType GetProfile(RequestBase<int> request);
        User GetByLogin(RequestBase<string> request);
        User GetById(int id);
    }
}
