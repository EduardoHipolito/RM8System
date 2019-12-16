using AutoMapper;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Web.Models
{
    public class LegalPersonModel : BaseModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string HomePage { get; set; }
        public string FantasyName { get; set; }
        public string CorporateName { get; set; }
        public int? IdCnae { get; set; }
    }
}
