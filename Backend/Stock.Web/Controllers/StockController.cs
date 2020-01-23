using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Business.Exceptions;
using Framework.Business.Request;
using Framework.Business.Response;
using Framework.Helpers;
using Framework.Web;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Stock.Business.Contracts;
using Stock.Domain;
using Stock.Web.Models;

namespace Stock.Web.Controllers
{
    public class StockController : CrudController<IStockBusiness, StockModel, Stock.Domain.Stock>
    {
        public StockController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public ActionResult GetSummary()
        {
            try
            {
                var Retorno = _business.GetSummary(this.CreateRequest<RequestBase>()).ToList();
                var mappedReturn = Retorno.Select(s => _mapper.Map<StockSumModel>(s)).ToList();
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = mappedReturn
                });
            }
            catch (BusinessException ex)
            {
                Log.Instance.ErrorLog(ex);
                return Json(new ResponseResult
                {
                    State = ResponseState.Failed,
                    Msg = ex.Message
                });
            }
        }

        [HttpPost]
        public ActionResult GetHistoryByIdProduct([FromBody]RequestById request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestById>();
                requestBusiness.Id = request.Id;
                var Retorno = _business.GetHistoryByIdProduct(requestBusiness);
                var mappedReturn = Retorno.Select(s => _mapper.Map<StockHistoryModel>(s)).ToList();
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = mappedReturn
                });
            }
            catch (BusinessException ex)
            {
                Log.Instance.ErrorLog(ex);
                return Json(new ResponseResult
                {
                    State = ResponseState.Failed,
                    Msg = ex.Message
                });
            }
        }
    }
}
