using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IUserService
    {
        Task<UserModel> AuthenticateAsync(LoginModel loginModel);

        Task<UserModel> RegisterAsync(SignUpModel signUpModel);
    }
}
