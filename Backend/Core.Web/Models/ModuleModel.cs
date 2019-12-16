using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class ModuleModel : BaseModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Index { get; set; }
        public string Icon { get; set; }
        public List<AplicationModel> Aplications { get; set; }
        public string ModuleCode { get; set; }
    }
}
