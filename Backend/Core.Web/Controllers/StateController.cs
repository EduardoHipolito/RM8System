using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.Commands;
using Core.Business.Contracts;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Core.Web.Models;
using Framework.Business.Exceptions;
using Framework.Business.Request;
using Framework.Business.Response;
using Framework.Helpers;
using Framework.Web;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers
{
    public class StateController : CrudController<IStateBusiness, StateModel, State>
    {
        public StateController(IMapper mapper) : base(mapper)
        {
        }

        [HttpPost]
        public ActionResult GetByCountry([FromBody]RequestById request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestById>();
                requestBusiness.Id = request.Id;
                var Retorno = _business.FindByCountry(requestBusiness).ToList();
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
