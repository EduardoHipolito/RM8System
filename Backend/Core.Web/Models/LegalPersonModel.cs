using AutoMapper;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class LegalPersonModel : PersonModel
    {
        public string FantasyName { get; set; }
        public string CorporateName { get; set; }
        public int? IdCnae { get; set; }
        public CnaeModel FKCnae { get; set; }
        [IgnoreMap]
        public List<CompanyModel> Companies { get; set; }
    }
}
