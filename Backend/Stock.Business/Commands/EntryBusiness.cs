using Stock.Business.Contracts;
using Stock.DataAccess;
using Stock.DataAccess.Commands;
using Core.Domain;
using Framework.Business;
using Framework.Business.Factory;
using Framework.DataAccess;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Core.Business.Contracts;
using Stock.Domain;
using Framework.Business.Request;
using Framework.Business.Helpers;
using Stock.Business.Message.Requests;
using Framework.Business.Exceptions;
using Stock.Business.Entities;

namespace Stock.Business.Commands
{
    public class EntryBusiness : BusinessBase<Entry, EntryDataAccess>, IEntryBusiness
    {
        public override bool Add(RequestBase<Entry> request)
        {
            var response = base.Add(request);

            var stockComponent = FactoryComponent.GetInstance<IStockBusiness>();

            var addRequest = this.CreateRequest<CreateStockByEntryRequest, RequestBase<Entry>>(request);
            addRequest.Entry = request.Parameter;
            addRequest.StockType = Domain.Enum.StockType.Entry;

            stockComponent.CreateStock(addRequest);

            return response;
        }

        public override bool Delete(RequestBase<Entry> request)
        {
            throw new BusinessException("Você não pode deletar uma entrada");
        }
        public override bool Delete(RequestById request)
        {
            throw new BusinessException("Você não pode deletar uma entrada");
        }
        public override bool DeleteAll(RequestBase<List<Entry>> request)
        {
            throw new BusinessException("Você não pode deletar uma entrada");
        }
        public override bool Enable(RequestBase<Entry> request)
        {
            throw new BusinessException("Você não pode habilitar uma entrada");
        }
        public override bool Disable(RequestBase<Entry> request)
        {
            throw new BusinessException("Você não pode desabilitar uma entrada");
        }
        public override bool DisableAll(RequestBase<IEnumerable<Entry>> request)
        {
            throw new BusinessException("Você não pode desabilitar uma entrada");
        }
        public override bool EnableAll(RequestBase<IEnumerable<Entry>> request)
        {
            throw new BusinessException("Você não pode habilitar uma entrada");
        }

        public override bool IsFullUser(int UserId)
        {
            return FactoryComponent.GetInstance<IUserBusiness>().GetById(UserId).ProfileType == Core.Domain.Enum.ProfileType.Developer;
        }

        public override bool ValidatePermition(int UserId, int IdCompany)
        {
            return FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>().ValidatePermition(UserId, IdCompany, AplicationsCodes.ModuleCode, AplicationsCodes.Entry);
        }

        public bool CancelEntry(RequestBase<Entry> request)
        {
            request.Parameter.IsCanceled = true;

            return this.Update(request);
        }

        public SumByQuantity GetDailyEntries(RequestBase request)
        {
            DateTime startDateTime = DateTime.Today;
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);

            var list = this._dataAccess.GetAll(request.IdCompany).Where(s => s.CreateDate >= startDateTime && s.CreateDate <= endDateTime).GroupBy(g => g)
                .Select(s => new SumByQuantity { Sum = s.Sum(ss => ss.TotalPrice), Quantity = s.Count()}).ToList();

            return new SumByQuantity { Quantity = list.Count(), Sum = list.Sum(s => s.Sum) };
        }

        public SumByQuantity GetMonthEntries(RequestBase request)
        {
            var year = DateTime.Today.Year;
            var month = DateTime.Today.Month;

            var list = this._dataAccess.GetAll(request.IdCompany).Where(s => s.CreateDate.Year >= year && s.CreateDate.Month <= month).GroupBy(g => g)
                .Select(s => new SumByQuantity { Sum = s.Sum(ss => ss.TotalPrice), Quantity = s.Count() }).ToList();

            return new SumByQuantity { Quantity = list.Count(), Sum = list.Sum(s => s.Sum) };
        }
    }
}