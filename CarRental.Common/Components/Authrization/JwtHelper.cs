using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;
namespace CarRental.Common.Authrization
{
    public class JwtHelper
    {
        public static string CreateToken(Claim[] identity)
        {

            string issuer = AppSettings.Jwt.Issuer;
            string audience = AppSettings.Jwt.Audience;
            string secretKey = AppSettings.Jwt.SecretKey;
            Console.WriteLine(secretKey);
            DateTime start = DateTime.UtcNow;
            DateTime end = DateTime.UtcNow.AddMinutes(20);

            byte[] bytes = Encoding.UTF8.GetBytes(secretKey);

            SymmetricSecurityKey symmetricSecurityKey = new(bytes);
            SigningCredentials credentials = new(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer, audience, identity, start, end, credentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
