using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Business.Entities
{
    public class UserChangePassword
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmationPassword { get; set; }
        public Guid Token { get; set; }
    }
}
