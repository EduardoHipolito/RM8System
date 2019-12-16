using Core.Domain.Enum;
using Framework.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Models
{
    public class UserChangePasswordModel : BaseModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmationPassword { get; set; }
        public Guid Token { get; set; }
    }
}
