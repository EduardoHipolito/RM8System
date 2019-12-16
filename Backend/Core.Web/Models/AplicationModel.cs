using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class AplicationModel : BaseModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Link { get; set; }

        public int? Index { get; set; }

        public int? IdModule { get; set; }
        public ModuleModel FKModule { get; set; }

        public string AplicationCode { get; set; }

        public bool ShowMenu { get; set; } = true;
    }
}
