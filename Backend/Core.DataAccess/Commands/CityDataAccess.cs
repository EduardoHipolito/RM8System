using Core.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Core.DataAccess.Commands
{
    public class CityDataAccess : DataAccessBase<City, CoreContext>
    {
        public override IQueryable<City> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKCountry).Include(i => i.FKState);
        }

        public override IQueryable<City> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKCountry).Include(i => i.FKState);
        }
    }
}