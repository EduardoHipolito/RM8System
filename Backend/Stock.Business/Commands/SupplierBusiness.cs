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
    public class SupplierBusiness : BusinessBase<Supplier, SupplierDataAccess>, ISupplierBusiness
    {

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Supplier);
        }

        public override IList<Supplier> GetAll(RequestBase request)
        {
            var list = base.GetAll(request);

            using (var mapper = new EntityMapHelper<Supplier>(request, list))
            {
                mapper.Add<LegalPerson, ILegalPersonBusiness>(x => x.IdLegalPerson, x => x.FKLegalPerson, x => x.Id);

                mapper.Map();
            }

            return list;
        }

        public override ResponseGrid<Supplier> GetAllGrid(RequestGrid<JObject, JObject> request, IQueryable<Supplier> query = null)
        {
            query = this.GetAll(this.CreateRequest<RequestBase, RequestGrid<JObject, JObject>>(request)).AsQueryable<Supplier>();

            return base.GetAllGrid(request, query);
        }
    }
}