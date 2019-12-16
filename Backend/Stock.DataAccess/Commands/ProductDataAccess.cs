using Stock.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Stock.DataAccess.Commands
{
    public class ProductDataAccess : DataAccessBase<Product, StockContext>
    {
        public override IQueryable<Product> GetAll(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i=> i.FKCategory).AsNoTracking();
        }
        public override IQueryable<Product> GetAllGrid(int IdCompany = 0)
        {
            return base.GetAll(IdCompany).Include(i => i.FKCategory).AsNoTracking();
        }
    }
}
