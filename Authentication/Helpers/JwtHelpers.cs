using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authentication.Helpers
{
    public class JwtHelpers
    {
        public static string GenerateToken(int phoneNumber, string secret)
        {
            var _jwtTokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(UserIdClaim,phoneNumber.ToString())
                }),
                Expires = DateTime.Now.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = _jwtTokenHandler.CreateToken(tokenDescriptor);
            return _jwtTokenHandler.WriteToken(token);
        }

        public static string UserIdClaim = "UserId";
    }
}
