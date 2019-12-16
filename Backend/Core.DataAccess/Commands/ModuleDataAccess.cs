using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class ModuleDataAccess : DataAccessBase<Module, CoreContext>
    {
        public override IQueryable<Module> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.Aplications);
        }
        public override IQueryable<Module> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.Aplications);
        }
        public override void Update(Module obj)
        {
            foreach (var item in obj.Aplications)
            {
                item.ModifiedDate = DateTime.Now;
                Context.Entry(item).State = EntityState.Modified;
            }
            base.Update(obj);
        }
    }
}
