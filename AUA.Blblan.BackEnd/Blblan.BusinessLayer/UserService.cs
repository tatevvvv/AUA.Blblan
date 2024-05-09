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

        public async Task<UserModel> AuthenticateAsync(LoginModel loginModel)
        {
            var user = await _userManager.FindByNameAsync(loginModel.UserName);
            if (user == null)
            {
                // User not found, return null or throw an exception
                throw new InvalidOperationException("User not found");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            if (!isPasswordValid)
            {
                // Incorrect password, return null or throw an exception
                throw new InvalidOperationException("Incorrect Password");
            }

            var userRes = new UserModel
            {
                Id = user.Id,
                Email = user.Email
            };


            return userRes;
        }

        public async Task<UserModel> RegisterAsync(SignUpModel signUpModel)
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

            var userRes = new UserModel
            {
                Id = newUser.Id,
                Email = newUser.Email
            };

            return userRes;
        }
    }
}
