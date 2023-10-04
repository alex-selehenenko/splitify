using Microsoft.IdentityModel.Tokens;
using Splitify.Identity.Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Splitify.Identity.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _options;

        public JwtService(JwtOptions options)
        {
            _options = options;
        }

        public string Generate(string userId, UserRole userRole)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.UTF8.GetBytes(_options.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimType.Sub, userId),
                    new Claim(ClaimType.Role, userRole.ToString().ToLowerInvariant()),
                    new Claim(ClaimType.Aud, _options.Audience),
                    new Claim(ClaimType.Iss, _options.Issuer)
                }),
                Expires = DateTime.UtcNow.AddMilliseconds(_options.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public abstract class ClaimType
        {
            public const string Role = "role";

            public const string Sub = "sub";

            public const string Iss = "iss";

            public const string Aud = "aud";
        }
    }
}
