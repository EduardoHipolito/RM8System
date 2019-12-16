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
using Framework.Business.Response;
using Framework.Web;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Core.Web.Controllers
{
    public class AplicationController : CrudController<IAplicationBusiness, AplicationModel, Aplication>
    {
        public AplicationController(IMapper mapper) : base(mapper)
        {
        }
    }
}
