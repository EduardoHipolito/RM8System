using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class CityModel : BaseModel
    {
        public string Name { get; set; }
        public int? IdCountry { get; set; }
        public CountryModel FKCountry { get; set; }
        public int? IdState { get; set; }
        public StateModel FKState { get; set; }
    }
}
