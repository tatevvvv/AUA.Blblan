using Blblan.Common.Models;

namespace Blblan.Common.Services
{
    public interface IUserService
    {
        Task<User> AuthenticateAsync(string username, string password);

        Task<User> RegisterAsync(string username, string password);
    }
}
