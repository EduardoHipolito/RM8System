using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Framework.Web.Auth
{

    public class TokenAuthOption
    {
        public static string Audience { get; } = "EduardoHipolito";
        public static string Issuer { get; } = "EduardoHipolito";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);
        public string TokenType { get; } = "Bearer";

        public string GenerateToken(UserLoginViewModel c, DateTime expires, string Tipo)
        {
            var handler = new JwtSecurityTokenHandler();


            var claim1 = new Claim(ClaimTypes.Name, c.UserName);
            var claimId = new Claim("UserId", c.Id.ToString());
            var claimIdCompany = new Claim("IdCompany", string.Empty, System.Security.Claims.ClaimValueTypes.Integer32);
            var claimTipo = new Claim("Tipo", Tipo);
            var claims = new Claim[] { claim1, claimId, claimIdCompany, claimTipo };
            var claimsIdentity = new ClaimsIdentity(claims, "Bearer");

            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = Issuer,
                Audience = Audience,
                SigningCredentials = SigningCredentials,
                Subject = claimsIdentity,
                NotBefore = DateTime.Now,
                Expires = expires
            });

            return handler.WriteToken(securityToken);
        }
    }
}
