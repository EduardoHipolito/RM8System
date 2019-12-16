using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.Commands;
using Core.Business.Contracts;
using Core.Business.Entities;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Core.Web.Models;
using Framework.Business.Exceptions;
using Framework.Business.Request;
using Framework.Business.Response;
using Framework.Web;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers
{
    public class UserController : CrudController<IUserBusiness, UserModel, User>
    {
        public UserController(IMapper mapper) : base(mapper)
        {
        }

        [HttpPut]
        public ActionResult ChangePassword([FromBody]RequestBase<UserChangePasswordModel> request)
        {
            try
            {
                var businessRequest = new RequestBase<UserChangePassword>();
                businessRequest.UserId = request.UserId;
                businessRequest.IdCompany = request.IdCompany;
                businessRequest.Parameter = _mapper.Map<UserChangePassword>(request);

                if (_business.ChangePassword(businessRequest))
                {
                    var Retorno = "Senha altarada";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Msg = Retorno
                    });
                }
                else
                {
                    var Retorno = "Não foi possivel alterar a senha do usuario, consulte a área tecnica";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Failed,
                        Msg = Retorno
                    });
                }
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

        [HttpPut]
        public ActionResult ChangePasswordByToken([FromBody]RequestBase<UserChangePasswordModel> request)
        {
            try
            {
                var businessRequest = new RequestBase<UserChangePassword>();
                businessRequest.UserId = request.UserId;
                businessRequest.IdCompany = request.IdCompany;
                businessRequest.Parameter = _mapper.Map<UserChangePassword>(request);

                if (_business.ChangePasswordByToken(businessRequest))
                {
                    var Retorno = "Senha altarada";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Msg = Retorno
                    });
                }
                else
                {
                    var Retorno = "Não foi possivel alterar a senha do usuario, consulte a área tecnica";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Failed,
                        Msg = Retorno
                    });
                }
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

        [HttpPost]
        public ActionResult GetNewPassHash([FromBody]RequestBase<string> request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<string>>();
                requestBusiness.Parameter = request.Parameter;
                var Retorno = _business.GetNewPassHash(requestBusiness);
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

        [HttpPost]
        public ActionResult LoginAlreadyRegistered([FromBody]RequestBase<string> request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<string>>();
                requestBusiness.Parameter = request.Parameter;
                _business.LoginAlreadyRegistered(requestBusiness);
                var Retorno = "Login liberado";
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

        [HttpPost]
        public ActionResult GetByIdPerson([FromBody]RequestBase<int> request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<int>>();
                requestBusiness.Parameter = request.Parameter;
                var Retorno = _business.GetByIdPerson(requestBusiness);
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

        [HttpPost]
        public ActionResult GetPerLogin([FromBody]RequestBase<string> request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<string>>();
                requestBusiness.Parameter = request.Parameter;
                var Retorno = _business.GetByLogin(requestBusiness);
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
