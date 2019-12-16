using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class AplicationDataAccess : DataAccessBase<Aplication, CoreContext>
    {
        public override IQueryable<Aplication> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKModule);
        }

        public override IQueryable<Aplication> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKModule);
        }

        public IQueryable<Module> Include(IQueryable<Module> query)
        {
            return query.Include(i => i.Aplications);
        }
    }
}
