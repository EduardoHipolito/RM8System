using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class LegalPersonDataAccess : DataAccessBase<LegalPerson, CoreContext>
    {
        public override IQueryable<LegalPerson> GetAll(int IdCompany = 0)
        {
            var legalPerson = Context.Person.OfType<LegalPerson>();
            if (IdCompany > 0)
            {
                legalPerson.Where(x => x.IdCompanyPermition == IdCompany);
            }
            return legalPerson.Include(i => i.Documents).Include(i => i.Phones).Include(i => i.Addresses).AsNoTracking();
        }
        public override IQueryable<LegalPerson> GetAllGrid(int IdCompany = 0)
        {
            var legalPerson = Context.Person.OfType<LegalPerson>();
            if (IdCompany > 0)
            {
                legalPerson.Where(x => x.IdCompanyPermition == IdCompany);
            }
            return legalPerson.Include(i => i.Documents).Include(i => i.Phones).Include(i => i.Addresses).AsNoTracking();
        }
    }
}
