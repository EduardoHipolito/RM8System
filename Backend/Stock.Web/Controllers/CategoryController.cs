using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Framework.Business.Response;
using Framework.Web;
using Framework.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Stock.Business.Contracts;
using Stock.Domain;
using Stock.Web.Models;

namespace Stock.Web.Controllers
{
    public class CategoryController : CrudController<ICategoryBusiness, CategoryModel, Category>
    {
        public CategoryController(IMapper mapper) : base(mapper)
        {
        }
    }
}
