﻿using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Stock.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Stock.Business.Contracts
{
    [FactoryReference("Stock.Business.Commands.CustomerBusiness, Stock.Business")]
    public interface ICustomerBusiness : IBusinessBase<Customer>
    {

    }
}
