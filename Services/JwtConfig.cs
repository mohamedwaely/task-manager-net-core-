using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using task_manager.Data;

namespace task_manager.Services
{
    public class JwtConfig
    {
        private JWTSettings _settings;
        public JwtConfig(IOptions<JWTSettings> settings) {
            _settings = settings.Value;
        }

        public string generateToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("email", user.Email),
                new Claim("role", "user")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "practice.com",
                    audience: "practice APIs",
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(60),
                    signingCredentials: creds
                    );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
