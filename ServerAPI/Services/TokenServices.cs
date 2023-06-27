using Microsoft.IdentityModel.Tokens;
using ServerAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerAPI.Services
{
    public class TokenServices
    {
        public string GerarToken(Usuario usuario)
        {
            var manipularToken = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Configuration.JWTKey);


            var tokenDescriptior = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new (ClaimTypes.Name, usuario.Login),
                    new ("Id", usuario.ID.ToString()),
                    new (ClaimTypes.Role, usuario.Acesso.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(8),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = manipularToken.CreateToken(tokenDescriptior);
            return manipularToken.WriteToken(token);
        }
    }
}
