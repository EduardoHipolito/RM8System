using AutoMapper;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class CnaeModel : BaseModel
    {
        public int? Code { get; set; }
        public string Description { get; set; }
        [IgnoreMap]
        public List<LegalPersonModel> LegalPeople { get; set; }
    }
}
