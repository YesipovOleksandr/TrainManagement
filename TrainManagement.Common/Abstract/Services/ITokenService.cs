using TrainManagement.Common.Models;
using System.Security.Claims;

namespace TrainManagement.Common.Abstract.Services
{
    public interface ITokenService
    {
        string GenerateAccessToken(User model);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
