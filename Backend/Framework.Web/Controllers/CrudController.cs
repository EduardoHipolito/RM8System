using AutoMapper;
using Framework.Business;
using Framework.Business.Exceptions;
using Framework.Business.Factory;
using Framework.Business.Interfaces;
using Framework.Business.Request;
using Framework.Business.Response;
using Framework.DataAccess;
using Framework.Domain;
using Framework.Helpers;
using Framework.Web.Filtes;
using Framework.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Framework.Web.Controllers
{
    //[HeadersActionFilter]
    public class CrudController<TBusiness, TModel, TEntity> : BaseController
    where TEntity : EntityBase
    where TModel : BaseModel
    where TBusiness : class, IBusinessBase<TEntity>
    {
        public readonly IMapper _mapper;
        public readonly TBusiness _business;//= FactoryComponent.GetInstance<TBusiness>();

        public CrudController(IMapper mapper)
        {
            _business = FactoryComponent.GetInstance<TBusiness>();
            _mapper = mapper;
        }


        [HttpPost]
        public ActionResult GetAllGrid([FromBody]RequestGrid<JObject, JObject> request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestGrid<JObject, JObject>>();
                requestBusiness.Settings = request.Settings;
                requestBusiness.Parameter = request.Parameter;
                var Retorno = _business.GetAllGrid(requestBusiness);
                var response = new ResponseGridModel<TModel>();
                response.DataList = Retorno.DataList.Select(s => _mapper.Map<TModel>(s)).ToList();
                response.TotalRecords = Retorno.TotalRecords;

                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = response
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
        public ActionResult Get([FromBody]RequestById request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestById>();
                requestBusiness.Id = request.Id;
                var Retorno = _business.Get(requestBusiness);

                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = _mapper.Map<TModel>(Retorno)
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
        public ActionResult GetAll()
        {
            try
            {
                var Retorno = _business.GetAll(this.CreateRequest<RequestBase>()).ToList();
                var mappedReturn = Retorno.Select(s => _mapper.Map<TModel>(s)).ToList();
                return Json(new ResponseResult
                {
                    State = ResponseState.Success,
                    Data = mappedReturn
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
        virtual public ActionResult Add([FromBody]RequestBase<TModel> requestModel)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<TEntity>>();
                requestBusiness.Parameter = _mapper.Map<TEntity>(requestModel.Parameter);

                if (_business.Add(requestBusiness))
                {
                    const string Retorno = "TEntity inserida com sucesso";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Msg = Retorno
                    });
                }
                else
                {
                    var Retorno = "Não foi possivel inserir a TEntity, consulte a área tecnica";

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
        public ActionResult Update([FromBody]RequestBase<TModel> requestModel)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<TEntity>>();
                requestBusiness.Parameter = _mapper.Map<TEntity>(requestModel.Parameter);
                if (_business.Update(requestBusiness))
                {
                    var Mensagem = "TEntity alterada";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Msg = Mensagem
                    });
                }
                else
                {
                    var Mensagem = "Não foi possivel alterar a TEntity, consulte a área tecnica";

                    return Json(new ResponseResult
                    {
                        State = ResponseState.Failed,
                        Msg = Mensagem
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
        public ActionResult Delete([FromBody]RequestById request)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestById>();
                requestBusiness.Id = request.Id;
                if (_business.Delete(requestBusiness))
                {
                    var Retorno = "TEntity deletada com sucesso";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Msg = Retorno
                    });
                }
                else
                {
                    var Retorno = "Não foi possivel deletar a TEntity, consulte a área tecnica";

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
        public ActionResult Delete([FromBody]RequestBase<TModel> requestModel)
        {
            try
            {
                var requestBusiness = this.CreateRequest<RequestBase<TEntity>>();
                requestBusiness.Parameter = _mapper.Map<TEntity>(requestModel.Parameter);
                if (_business.Delete(requestBusiness))
                {
                    var Retorno = "TEntity deletada com sucesso";
                    return Json(new ResponseResult
                    {
                        State = ResponseState.Success,
                        Msg = Retorno
                    });
                }
                else
                {
                    var Retorno = "Não foi possivel deletar a TEntity, consulte a área tecnica";

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
    }
}
