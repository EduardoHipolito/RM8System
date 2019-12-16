using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class CountryModel : BaseModel
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int? PhoneCode { get; set; }
        public List<CityModel> Cities { get; set; }
        public List<StateModel> States { get; set; }
    }
}
