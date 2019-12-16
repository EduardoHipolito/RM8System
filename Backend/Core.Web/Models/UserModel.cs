using Core.Domain.Enum;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class UserModel : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public ProfileType? ProfileType { get; set; }
        public Guid TokenAlteracaoDeSenha { get; set; }
        public int? IdPerson { get; set; }
        public PhysicalPersonModel FKPerson { get; set; }
        public ICollection<UserAplicationCompanyModel> Permitions { get; set; }
    }
}
