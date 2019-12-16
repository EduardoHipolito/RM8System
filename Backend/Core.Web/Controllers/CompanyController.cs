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
using Framework.Web;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers
{
    public class CompanyController : CrudController<ICompanyBusiness, CompanyModel, Company>
    {
        public CompanyController(IMapper mapper) : base(mapper)
        {
        }

        [HttpGet]
        public ActionResult GetCompanyByUser()
        {
            try
            {
                var Retorno = _business.CompanyByUsers(this.CreateRequest<RequestBase>()).ToList();
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = Retorno
                });
            }
            catch (BusinessException ex)
            {
                return Json(new ResponseResult
                {
                    State = ResponseState.Failed,
                    Msg = ex.Message
                });
            }
        }

        [HttpGet]
        public ActionResult GetAllMasters()
        {
            try
            {
                var Retorno = _business.GetAllMasters(this.CreateRequest<RequestBase>()).ToList();
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = Retorno
                });
            }
            catch (BusinessException ex)
            {
                return Json(new ResponseResult
                {
                    State = ResponseState.Failed,
                    Msg = ex.Message
                });
            }
        }
    }
}
