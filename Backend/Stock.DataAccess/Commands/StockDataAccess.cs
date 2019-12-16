using Stock.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Stock.DataAccess.Commands
{
    public class StockDataAccess : DataAccessBase<Stock.Domain.Stock, StockContext>
    {

    }
}