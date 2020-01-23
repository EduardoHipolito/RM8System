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
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers
{
    public class AddressController : CrudController<IAddressBusiness, AddressModel, Address>
    {
        public AddressController(IMapper mapper) : base(mapper)
        {
        }
        [HttpPost]
        public ActionResult GetByCEP([FromBody]RequestBase<string> request)
        {
            try
            {
                //string Retorno;
                //var request = WebRequest.Create("https://webmaniabr.com/api/1/cep/" + postal_cod + "/?app_key=3UvyeDwYQTQ3SDyGLaTBASYv7QcAD2FH&app_secret=H2CQhA6ybfieCdnFHmcyjSABoAHCHCzceHDgUUOf6EzJL3yp");
                //request.ContentType = "application/json; charset=utf-8";
                //var response = (HttpWebResponse)request.BeginGetResponse ();

                //using (var sr = new StreamReader(response.GetResponseStream()))
                //{
                //    Retorno = sr.ReadToEnd();
                //}
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = ""
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
