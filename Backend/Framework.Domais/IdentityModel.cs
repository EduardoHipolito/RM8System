using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Framework.Domain
{
    public class IdentityModel
    {
        public int Id { get; set; }
        public int IdCompany { get; set; }
        public string UserName { get; set; }
        public string ClainName { get; set; }
        public bool IsAuthenticated { get; set; }
        public List<Claim> Claims { get; set; }
        public string AuthenticationType { get; set; }
        public ClaimsIdentity Actor { get; set; }
    }
}
