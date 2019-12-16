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
using Framework.Business.Response;
using Newtonsoft.Json.Linq;
using System.Linq;

namespace Stock.Business.Commands
{
    public class CustomerBusiness : BusinessBase<Customer, CustomerDataAccess>, ICustomerBusiness
    {

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Customer);
        }

        public override IList<Customer> GetAll(RequestBase request)
        {
            var list = base.GetAll(request);

            using (var mapper = new EntityMapHelper<Customer>( request, list))
            {
                mapper.Add<PhysicalPerson, IPhysicalPersonBusiness>(x => x.IdPhysicalPerson, x => x.FKPhysicalPerson, x => x.Id);

                mapper.Map();
            }

            return list;
        }

        public override ResponseGrid<Customer> GetAllGrid(RequestGrid<JObject, JObject> request, IQueryable<Customer> query = null)
        {
            query = this.GetAll(this.CreateRequest<RequestBase, RequestGrid<JObject, JObject>>(request)).AsQueryable<Customer>();

            return base.GetAllGrid(request, query);
        }
    }
}