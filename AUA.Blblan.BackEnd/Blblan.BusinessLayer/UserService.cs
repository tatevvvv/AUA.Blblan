using Blblan.Common.Models;
using Blblan.Common.Services;
using Blblan.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace Blblan.BusinessLayer
{
    internal class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserResponse> AuthenticateAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user == null)
            {
                throw new InvalidOperationException("User not found");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!isPasswordValid)
            {
                throw new InvalidOperationException("Incorrect Password");
            }

            var userRes = new UserResponse()
            {
                Id = user.Id,
                UserName = user.UserName
            };


            return userRes;
        }

        public async Task<UserResponse> RegisterAsync(SignUpModel signUpModel)
        {
            var existingUser = await _userManager.FindByEmailAsync(signUpModel.Email);

            if (existingUser != null)
            {
                throw new InvalidOperationException("User with email already exists");
            }

            var newUser = new User
            {
                UserName = signUpModel.UserName,
                FullName = signUpModel.UserName,
                Email = signUpModel.Email,
            };

            var result = await _userManager.CreateAsync(newUser, signUpModel.Password);

            if (!result.Succeeded)
            {
                // Handle errors occurred during user creation
                throw new InvalidOperationException("Failed to register user");
            }

            var userRes = new UserResponse()
            {
                Id = newUser.Id,
                UserName = newUser.UserName
            };

            return userRes;
        }
    }
}
