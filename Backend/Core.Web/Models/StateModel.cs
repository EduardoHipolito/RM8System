using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class StateModel : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? IdCountry { get; set; }
        public CountryModel FKCountry { get; set; }
        public List<CityModel> Cities { get; set; }
    }
}
