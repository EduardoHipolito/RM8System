using Stock.Business.Contracts;
using Stock.DataAccess.Commands;
using Core.Domain;
using Framework.Business;
using Framework.Business.Factory;
using System.Collections.Generic;
using Core.Business.Contracts;
using Framework.Business.Request;
using Framework.Business.Helpers;
using Stock.Domain.Enum;
using Stock.Business.Message.Requests;
using Stock.Business.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Stock.Domain;

namespace Stock.Business.Commands
{
    public class StockBusiness : BusinessBase<Stock.Domain.Stock, StockDataAccess>, IStockBusiness
    {
        public List<Domain.Stock> CreateStock(CreateStockByEntryRequest request)
        {
            var newStocks = new List<Domain.Stock>();

            foreach (var product in request.Entry.ProductEntries)
            {
                newStocks.Add(new Domain.Stock
                {
                    IdEntry = request.Entry.Id,
                    IdProduct = product.Id,
                    Quantity = product.Quantity,
                    StockType = request.StockType
                });
            }
            var addRequest = this.CreateRequest<RequestBase<IEnumerable<Stock.Domain.Stock>>, CreateStockByEntryRequest>(request);
            addRequest.Parameter = newStocks;
            this.AddAll(addRequest);

            return newStocks;
        }

        public List<Domain.Stock> CreateStock(CreateStockBySaleRequest request)
        {
            var newStocks = new List<Domain.Stock>();

            foreach (var product in request.Sale.Products)
            {
                newStocks.Add(new Domain.Stock
                {
                    IdSale = request.Sale.Id,
                    IdProduct = product.Id,
                    Quantity = product.Quantity,
                    StockType = request.StockType
                });
            }
            var addRequest = this.CreateRequest<RequestBase<IEnumerable<Stock.Domain.Stock>>, CreateStockBySaleRequest>(request);
            addRequest.Parameter = newStocks;
            this.AddAll(addRequest);

            return newStocks;
        }

        public Domain.Stock Hit(HitStockRequest request)
        {
            var stock = new Domain.Stock
            {
                IdProduct = request.Product.Id,
                Quantity = request.Quantity,
                StockType = StockType.StockRit,
                StockHitType = request.StockHitType
            };

            var addRequest = this.CreateRequest<RequestBase<Stock.Domain.Stock>, HitStockRequest>(request);
            addRequest.Parameter = stock;
            this.Add(addRequest);

            return stock;
        }

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Stock);
        }

        public IList<StockHistory> GetHistoryByIdProduct(RequestById request)
        {
            var stockQuery = this._dataAccess.GetAll(request.IdCompany).Where(w => w.IdProduct == request.Id).Include(i => i.FKProduct).Include(i => i.FKEntry.FKSupplier).Include(i => i.FKSale.FKCustomer).AsNoTracking().ToList();

            using (var mapper = new EntityMapHelper<Customer>( request, stockQuery.Where(w => w.IdSale != null).Where(w => w.FKSale.IdCustomer != null).Select(s => s.FKSale.FKCustomer)))
            {
                mapper.Add<PhysicalPerson, IPhysicalPersonBusiness>(x => x.IdPhysicalPerson, x => x.FKPhysicalPerson, x => x.Id);
                mapper.Map();
            }
            using (var mapper = new EntityMapHelper<Supplier>(request, stockQuery.Where(w => w.IdEntry != null).Where(w => w.FKEntry.IdSupplier != null).Select(s => s.FKEntry.FKSupplier)))
            {
                mapper.Add<LegalPerson, ILegalPersonBusiness>(x => x.IdLegalPerson, x => x.FKLegalPerson, x => x.Id);
                mapper.Map();
            }

            return (from a in stockQuery
                    select new StockHistory
                    {
                        IdProduct = a.IdProduct,
                        ProductName = a.FKProduct.Name,
                        CreateDate = a.CreateDate,
                        Quantity =
                        a.StockType == StockType.Entry ? a.Quantity :
                        a.StockType == StockType.Sale ? a.Quantity * -1 :
                        a.StockType == StockType.CancelSale ? a.Quantity :
                        a.StockType == StockType.CancelEntry ? a.Quantity * -1 :
                        a.StockType == StockType.StockRit ? (a.StockHitType == StockHitType.Up ? a.Quantity : a.Quantity * -1) : 0,
                        CustomerName = (a.StockType == StockType.Sale || a.StockType == StockType.CancelSale) ? a.FKSale.FKCustomer.FKPhysicalPerson.Name : "",
                        SupplierName = (a.StockType == StockType.Entry || a.StockType == StockType.CancelEntry) ? a.FKEntry.FKSupplier.FKLegalPerson.CorporateName : "",
                        StockHitType = a.StockHitType,
                        StockType = a.StockType

                    }).ToList();
        }

        public IList<StockSum> GetSummary(RequestBase request)
        {
            var stockQuery = this._dataAccess.GetAll(request.IdCompany).Include(i => i.FKProduct).AsNoTracking();

            int count = 0;
            return (from a in stockQuery
                    select new 
                    {
                        a.FKProduct,
                        a.CreateDate,
                        Quantity =
                        a.StockType == StockType.Entry ? a.Quantity :
                        a.StockType == StockType.Sale ? a.Quantity * -1 :
                        a.StockType == StockType.CancelSale ? a.Quantity :
                        a.StockType == StockType.CancelEntry ? a.Quantity * -1 :
                        a.StockType == StockType.StockRit ? (a.StockHitType == StockHitType.Up ? a.Quantity : a.Quantity * -1) : 0
                    }).GroupBy(g => new { g.FKProduct })
                           .Select(s => new StockSum
                           {
                               Id = s.First().FKProduct.Id,
                               BarCode = s.First().FKProduct.BarCode,
                               Brand = s.First().FKProduct.Brand,
                               InternalNumber = s.First().FKProduct.InternalNumber,
                               Picture = s.First().FKProduct.Picture,
                               ProductName = s.First().FKProduct.Name,
                               IdProduct = s.First().FKProduct.Id,
                               Quantity = s.Sum(sum => sum.Quantity),
                               CreateDate = s.Max(m => m.CreateDate)
                           }).ToList();
        }
    }
}