﻿using Stock.Domain;
using Framework.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Stock.DataAccess.Commands
{
    public class CustomerDataAccess : DataAccessBase<Customer, StockContext>
    {

    }
}