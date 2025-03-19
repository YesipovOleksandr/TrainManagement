using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using TrainManagement.Common.Abstract.Services;
using TrainManagement.Common.Models;
using TrainManagement.Common.Models.Settings;

namespace TrainManagement.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTOptions _jWTOptions;

        public TokenService(IOptions<AppSettings> appSettings)
        {
            _jWTOptions = appSettings.Value.JWTOptions;
        }

        public string GenerateAccessToken(User model)
        {
            var securityKey = _jWTOptions.Secret;
            var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(securityKey)), SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,model.Id.ToString()),
                new Claim(ClaimTypes.Email,model.Login),
                new Claim(ClaimTypes.Role, model.Role.ToString())
            };


            var token = new JwtSecurityToken(_jWTOptions.Issuer,
                _jWTOptions.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(_jWTOptions.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jWTOptions.Secret)),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");
            return principal;
        }
    }
}
