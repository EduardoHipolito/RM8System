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
    public class UserAplicationCompanyController : CrudController<IUserAplicationCompanyBusiness, UserAplicationCompanyModel, UserAplicationCompany>
    {
        public UserAplicationCompanyController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public ActionResult GetMenu()
        {
            try
            {
                var Retorno = _business.GetMenuApplication(this.CreateRequest<RequestBase>()).ToList();
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
        public ActionResult GetRoles()
        {
            try
            {
                var Retorno = _business.GetAllRoles(this.CreateRequest<RequestBase>()).ToList();
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
