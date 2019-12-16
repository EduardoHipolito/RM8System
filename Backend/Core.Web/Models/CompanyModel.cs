using AutoMapper;
using Core.Domain.Enum;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class CompanyModel : BaseModel
    {
        public CompanyType? Type { get; set; }
        public int? PaymentDay { get; set; }
        public int? IdPerson { get; set; }
        public LegalPersonModel FKPerson { get; private set; }
        public int? IdMaster { get; set; }
        [IgnoreMap]
        public CompanyModel FKMaster { get; set; }
        public string ReducedName { get; set; }
        public List<UserAplicationCompanyModel> Permitions { get; set; }
        [IgnoreMap]
        public List<CompanyModel> Children { get; set; }
    }
}
