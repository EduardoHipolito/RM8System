using Stock.Business.Contracts;
using Stock.DataAccess;
using Stock.DataAccess.Commands;
using Core.Domain;
using Framework.Business;
using Framework.Business.Factory;
using Framework.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Business.Contracts;
using Stock.Domain;
using Framework.Business.Request;
using Framework.Business.Helpers;
using Stock.Business.Message.Requests;
using System.Linq;
using Stock.Business.Entities;

namespace Stock.Business.Commands
{
    public class SaleBusiness : BusinessBase<Sale, SaleDataAccess>, ISaleBusiness
    {
        public override bool Add(RequestBase<Sale> request)
        {
            request.Parameter.FKCustomer = null;
            foreach (var item in request.Parameter.Payments)
            {
                item.FKFormOfPayment = null;
            }
            foreach (var item in request.Parameter.Products)
            {
                item.FKProduct = null;
            }

            var response = base.Add(request);

            var stockComponent = FactoryComponent.GetInstance<IStockBusiness>();

            var addRequest = this.CreateRequest<CreateStockBySaleRequest, RequestBase<Sale>>(request);
            addRequest.Sale = request.Parameter;
            addRequest.StockType = Domain.Enum.StockType.Sale;

            stockComponent.CreateStock(addRequest);

            return response;
        }

        public SumByQuantity GetDailyProfit(RequestBase request)
        {
            DateTime startDateTime = DateTime.Today;
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

            var list = (from s in this._dataAccess.GetAll(request.IdCompany)
                        join ps in this._dataAccess.Context.ProductSale on s.Id equals ps.SaleId
                        join p in this._dataAccess.Context.Product on ps.IdProduct equals p.Id
                        join pe in this._dataAccess.Context.ProductEntry on p.Id equals pe.IdProduct
                        where (s.CreateDate >= startDateTime && s.CreateDate <= endDateTime)
                        group new { s.Discount, ps.Quantity, pe.UnitPrice, p.Price } by p.Id into g
                        select (((g.Sum(s => s.Price) * g.Sum(s => s.Quantity)) - g.Sum(s => s.Discount)) - (g.Sum(s => s.UnitPrice) * g.Sum(s => s.Quantity)))).ToList().GroupBy(g => g)
                .Select(s => new SumByQuantity { Sum = s.Sum(), Quantity = s.Count() });

            return new SumByQuantity { Quantity = list.Count(), Sum = list.Sum(s => s.Sum) };
        }

        public SumByQuantity GetDailySalesAmout(RequestBase request)
        {
            DateTime startDateTime = DateTime.Today;
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

            var list = (from s in this._dataAccess.GetAll(request.IdCompany)
                    join ps in this._dataAccess.Context.ProductSale on s.Id equals ps.SaleId
                    join p in this._dataAccess.Context.Product on ps.IdProduct equals p.Id
                    where (s.CreateDate >= startDateTime && s.CreateDate <= endDateTime)
                    group new { s.Discount, ps.Quantity, p.Price } by p.Id into g
                    select (((g.Sum(s => s.Price) * g.Sum(s => s.Quantity)) - g.Sum(s => s.Discount)))).ToList().GroupBy(g => g)
                .Select(s => new SumByQuantity { Sum = s.Sum(), Quantity = s.Count() });

            return new SumByQuantity { Quantity = list.Count(), Sum = list.Sum(s => s.Sum) };
        }

        public SumByQuantity GetMonthProfit(RequestBase request)
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;

            var list = (from s in this._dataAccess.GetAll(request.IdCompany)
                    join ps in this._dataAccess.Context.ProductSale on s.Id equals ps.SaleId
                    join p in this._dataAccess.Context.Product on ps.IdProduct equals p.Id
                    join pe in this._dataAccess.Context.ProductEntry on p.Id equals pe.IdProduct
                    where (s.CreateDate.Year >= year && s.CreateDate.Month <= month)
                    group new { s.Discount, ps.Quantity, pe.UnitPrice, p.Price } by p.Id into g
                    select (((g.Sum(s => s.Price) * g.Sum(s => s.Quantity)) - g.Sum(s => s.Discount)) - (g.Sum(s => s.UnitPrice) * g.Sum(s => s.Quantity)))).ToList().GroupBy(g => g)
                .Select(s => new SumByQuantity { Sum = s.Sum(), Quantity = s.Count() });

            return new SumByQuantity { Quantity = list.Count(), Sum = list.Sum(s => s.Sum) };
        }

        public SumByQuantity GetMonthSalesAmout(RequestBase request)
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;

            var list = (from s in this._dataAccess.GetAll(request.IdCompany)
                    join ps in this._dataAccess.Context.ProductSale on s.Id equals ps.SaleId
                    join p in this._dataAccess.Context.Product on ps.IdProduct equals p.Id
                    where (s.CreateDate.Year >= year && s.CreateDate.Month <= month)
                    group new { s.Discount, ps.Quantity, p.Price } by p.Id into g
                    select (((g.Sum(s => s.Price) * g.Sum(s => s.Quantity)) - g.Sum(s => s.Discount)))).ToList().GroupBy(g => g)
                .Select(s => new SumByQuantity { Sum = s.Sum(), Quantity = s.Count() });

            return new SumByQuantity { Quantity = list.Count(), Sum = list.Sum(s => s.Sum) };
        }

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Sale);
        }

    }
}