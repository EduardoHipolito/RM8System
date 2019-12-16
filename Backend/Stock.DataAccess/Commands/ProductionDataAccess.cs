using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using Stock.Domain;
using System.Linq;

namespace Stock.DataAccess.Commands
{
    public class ProductionDataAccess : DataAccessBase<Production, StockContext>
    {
        public override IQueryable<Production> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKFinalProduct).Include(i => i.ProductionStages).AsNoTracking();
        }
    }
}
