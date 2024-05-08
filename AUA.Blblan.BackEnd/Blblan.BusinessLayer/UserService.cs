using Blblan.Common.Models;
using Blblan.Common.Services;

namespace Blblan.BusinessLayer
{
    internal class UserService : IUserService
    {
        public Task<User> AuthenticateAsync(string username, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User> RegisterAsync(string username, string password)
        {
            throw new NotImplementedException();
        }
    }
}
