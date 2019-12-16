using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.DataAccess.Commands
{
    public class CompanyDataAccess : DataAccessBase<Company,CoreContext>
    {
        public IQueryable<Company> CompaniesByUser(int idUser)
        {
            IQueryable<Company> query = (from a in Context.Company
                                         join b in Context.Permitions.Include(i => i.FKAplication).Include(i => i.FKUser) on a.Id equals b.IdCompany
                                         where b.IdUser == idUser
                                         where b.Status == Framework.Domain.Enum.EntityType.Active
                                         where b.FKAplication.Status == Framework.Domain.Enum.EntityType.Active
                                         where b.FKUser.Status == Framework.Domain.Enum.EntityType.Active
                                         where a.Status == Framework.Domain.Enum.EntityType.Active
                                         select a);


            return query.GroupBy(a => a).Select(g => g.Key).OrderBy(x => x.Id);
        }

        public override IQueryable<Company> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKMaster).Include(i => i.FKPerson);
        }

        public override IQueryable<Company> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKMaster).Include(i => i.FKPerson);
        }
    }
}
