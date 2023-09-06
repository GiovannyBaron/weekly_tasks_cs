using System.IdentityModel.Tokens.Jwt;
using weekly_tasks.Models.AuthUser;

namespace weekly_tasks.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterRequest request);
        Task<string> Login(LoginRequest request);
    }
}
