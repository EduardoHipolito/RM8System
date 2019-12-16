using Stock.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Stock.DataAccess.Commands
{
    public class EntryDataAccess : DataAccessBase<Entry, StockContext>
    {
        public override IQueryable<Entry> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKSupplier).Include(i => i.ProductEntries).AsNoTracking();
        }
        public override IQueryable<Entry> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKSupplier).Include(i => i.ProductEntries).AsNoTracking();
        }
    }
}