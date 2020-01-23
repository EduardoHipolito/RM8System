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
    public class EntryController : CrudController<IEntryBusiness, EntryModel, Entry>
    {
        public EntryController(IMapper mapper) : base(mapper)
        {
        }
        [HttpGet]
        public ActionResult GetDailyEntries()
        {
            try
            {
                var Retorno = _business.GetDailyEntries(this.CreateRequest<RequestBase>());
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
        public ActionResult GetMonthEntries()
        {
            try
            {
                var Retorno = _business.GetMonthEntries(this.CreateRequest<RequestBase>());
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
