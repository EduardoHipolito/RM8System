using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class PhysicalPersonDataAccess : DataAccessBase<PhysicalPerson, CoreContext>
    {
        public override IQueryable<PhysicalPerson> GetAll(int IdCompany = 0)
        {
            var physicalPeople = Context.Person.OfType<PhysicalPerson>();
            if (IdCompany > 0)
            {
                physicalPeople.Where(x => x.IdCompanyPermition == IdCompany);
            }
            return physicalPeople.Include(i => i.Documents).Include(i => i.Phones).Include(i => i.Addresses).AsNoTracking();
        }
        public override IQueryable<PhysicalPerson> GetAllGrid(int IdCompany = 0)
        {
            var physicalPeople = Context.Person.OfType<PhysicalPerson>();
            if (IdCompany > 0)
            {
                physicalPeople.Where(x => x.IdCompanyPermition == IdCompany);
            }
            return physicalPeople.Include(i => i.Documents).Include(i => i.Phones).Include(i => i.Addresses).AsNoTracking();
        }
        public override void Delete(int id)
        {
            var del = Get(id);
            Commit();
            base.Delete(del);
        }
        public override void Update(PhysicalPerson obj)
        {
            foreach (var item in obj.Phones)
            {
                item.ModifiedDate = DateTime.Now;
                Context.Entry(item).State = EntityState.Modified;
            }

            foreach (var item in obj.Addresses)
            {
                item.ModifiedDate = DateTime.Now;
                Context.Entry(item).State = EntityState.Modified;
            }

            foreach (var item in obj.Documents)
            {
                item.ModifiedDate = DateTime.Now;
                Context.Entry(item).State = EntityState.Modified;
            }
            base.Update(obj);
        }
    }
}
