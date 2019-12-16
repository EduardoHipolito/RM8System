using Stock.Business.Contracts;
using Stock.DataAccess;
using Stock.DataAccess.Commands;
using Core.Domain;
using Framework.Business;
using Framework.Business.Factory;
using Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Business.Contracts;
using Stock.Domain;
using Framework.Business.Request;
using Framework.Business.Helpers;

namespace Stock.Business.Commands
{
    public class PaymentBusiness : BusinessBase<Payment, PaymentDataAccess>, IPaymentBusiness
    {

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Payment);
        }

    }
}