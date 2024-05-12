using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IUserService
    {
        Task<UserResponse> AuthenticateAsync(LoginModel loginModel);

        Task<UserResponse> RegisterAsync(SignUpModel signUpModel);
    }
}
