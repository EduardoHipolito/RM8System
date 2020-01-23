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
    public class SaleController : CrudController<ISaleBusiness, SaleModel, Sale>
    {
        public SaleController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public async Task<ActionResult> GetDailySalesAmout()
        {
            try
            {
                var Retorno = await _business.GetDailySalesAmout(this.CreateRequest<RequestBase>());
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = Retorno
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

        [HttpGet]
        public async Task<ActionResult> GetMonthSalesAmout()
        {
            try
            {
                var Retorno = await _business.GetMonthSalesAmout(this.CreateRequest<RequestBase>());
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = Retorno
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

        [HttpGet]
        public async Task<ActionResult> GetDailyProfit()
        {
            try
            {
                var Retorno = await _business.GetDailyProfit(this.CreateRequest<RequestBase>());
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = Retorno
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

        [HttpGet]
        public async Task<ActionResult> GetMonthProfit()
        {
            try
            {
                var Retorno = await _business.GetMonthProfit(this.CreateRequest<RequestBase>());
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = Retorno
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
