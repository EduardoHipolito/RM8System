using Core.Domain.Enum;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class UserAplicationCompanyModel : BaseModel
    {
        public int? IdAplication { get; set; }
        public AplicationModel FKAplication { get; set; }
        public int? IdUser { get; set; }
        public UserModel FKUser { get; set; }
        public int? IdCompany { get; set; }
        public CompanyModel FKCompany { get; set; }
        public AccessLevel? AccessLevel { get; set; }
        public string FKAplicationName { get; set; }
        public string FKUserName { get; set; }
        public string FKCompanyName { get; set; }
    }
}
