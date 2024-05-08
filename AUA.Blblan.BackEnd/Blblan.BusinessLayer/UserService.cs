using Blblan.Common.Models;
using Blblan.Common.Services;

namespace Blblan.BusinessLayer
{
    internal class UserService : IUserService
    {
        public Task<User> AuthenticateAsync(LoginModel loginModel)
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterAsync(SignUpModel signUpModel)
        {
            throw new NotImplementedException();
        }
    }
}
