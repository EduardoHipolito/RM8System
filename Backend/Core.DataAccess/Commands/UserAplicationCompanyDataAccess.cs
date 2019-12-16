using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class UserAplicationCompanyDataAccess : DataAccessBase<UserAplicationCompany, CoreContext>
    {
        public override IQueryable<UserAplicationCompany> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKAplication).Include(i => i.FKCompany).Include(i => i.FKUser).Include(i => i.FKUser.FKPerson).Include(i => i.FKCompany.Children);
        }
        public override IQueryable<UserAplicationCompany> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKAplication).Include(i => i.FKCompany).Include(i => i.FKUser).Include(i => i.FKUser.FKPerson).Include(i => i.FKCompany.Children);
        }

    }
}