using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IdentityApi.Applications
{
    public class Token(IConfiguration _configuration) : IToken
    {
        public string GenerateToken(string Id)
        {
            var TokenHandler = new JwtSecurityTokenHandler(); // Token Üretmemi ve üretirken ayarlamalar yapmamızı sağlayan sınıf.
            var Key = Encoding.ASCII.GetBytes(_configuration["SecretKey"].ToString());
            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                      new Claim(ClaimTypes.NameIdentifier,Id),
                      new Claim(ClaimTypes.Role, "Admin"),
                ]),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Key), SecurityAlgorithms.HmacSha256Signature)
            };
            return TokenHandler.WriteToken(TokenHandler.CreateToken(TokenDescriptor));
        }
    }
}
