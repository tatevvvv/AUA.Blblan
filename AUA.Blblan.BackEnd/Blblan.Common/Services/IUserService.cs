using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(LoginModel loginModel);

        Task<User> RegisterAsync(SignUpModel signUpModel);
    }
}
