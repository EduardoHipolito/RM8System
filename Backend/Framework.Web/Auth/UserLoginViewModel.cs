using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Web.Auth
{
    public class UserLoginViewModel
    {
        public int? Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
