using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Core.Business.Commands;
using Core.Business.Contracts;
using Core.Business.Message.Requests;
using Core.DataAccess;
using Core.DataAccess.Commands;
using Core.Domain;
using Core.Web.Models;
using Framework.Business.Exceptions;
using Framework.Business.Factory;
using Framework.Business.Request;
using Framework.Business.Response;
using Framework.Web;
using Framework.Web.Auth;
using Framework.Web.Controllers;
using Framework.Web.Filtes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.Web.Controllers
{
    [Route("[controller]/[Action]")]
    public class TokenController : BaseController
    {

        public readonly IUserBusiness business = FactoryComponent.GetInstance<IUserBusiness>();
        public readonly IUserAplicationCompanyBusiness userAplicationCompanyBusiness = FactoryComponent.GetInstance<IUserAplicationCompanyBusiness>();

        [HttpPost]
        public ActionResult Login([FromBody]UserLoginViewModel user)
        {
            try
            {
                int? IdUsuario = 0;
                if (business.Login(ref IdUsuario, user.UserName, user.Password))
                {
                    var _tokenAuthOption = new TokenAuthOption();
                    user.Id = IdUsuario;
                    var requestAt = DateTime.Now;
                    var expiresIn = requestAt + _tokenAuthOption.ExpiresSpan;
                    var token = _tokenAuthOption.GenerateToken(user, expiresIn, "access_token");
                    var refresh = _tokenAuthOption.GenerateToken(user, expiresIn.AddMinutes(10), "refresh_token");
                    var request = this.CreateRequest<RequestBase>();
                    request.UserId = IdUsuario??0;

                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Data = new
                        {
                            requertAt = requestAt,
                            expiresIn = _tokenAuthOption.ExpiresSpan.TotalSeconds,
                            tokeyType = _tokenAuthOption.TokenType,
                            access_token = token,
                            refresh_token = refresh,
                            UserId = IdUsuario
                        }
                    });
                }
                else
                {
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Failed,
                        Msg = "Username or password is invalid"
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

        [HttpGet]
        [Authorize("Bearer")]
        [ClaimsActionFilter]
        public string ValidateAutentication(string OperationType, string ModuleName, string ControllerName)
        {
            var request = this.CreateRequest<AuthorizeAplicationRequest>();
            request.AplicationCode = ControllerName.ToUpper();
            request.ModuleCode = ModuleName.ToUpper();
            request.OperationType = OperationType.ToUpper();

            var isAuthorized = userAplicationCompanyBusiness.AuthorizeAplication(request);
            return JsonConvert.SerializeObject(new ResponseResult
            {
                State = isAuthorized ? ResponseState.Success : ResponseState.NotAuth,
                Data = new
                {
                    IdCompany = request.IdCompany,
                    UserId = request.UserId
                }
            });
        }
    }
}
