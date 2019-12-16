using AutoMapper;
using Framework.Web.Controllers;
using Stock.Business.Contracts;
using Stock.Domain;
using Stock.Web.Models;

namespace Stock.Web.Controllers
{
    public class ProductionController : CrudController<IProductionBusiness, ProductionModel, Production>
    {
        public ProductionController(IMapper mapper) : base(mapper)
        {
        }
    }
}
