using System;
using System.Threading.Tasks;
using Blblan.Common.Models;
using Blblan.Common.Services;
using Blblan.Data.Entities;
using Blblan.Data.Repositories;
using Microsoft.AspNetCore.Identity;

namespace Blblan.BusinessLayer
{
    internal class UserService : IUserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserModel> AuthenticateAsync(LoginModel loginModel)
        {
            // var user = await _userRepository.FindByUsernameAsync(loginModel.UserName);
            // if (user == null)
            // {
            //     // User not found, return null or throw an exception
            //     return null;
            // }

            // var isPasswordValid = await _userManager.CheckPasswordAsync(user, loginModel.Password);
            // if (!isPasswordValid)
            // {
            //     // Incorrect password, return null or throw an exception
            //     return null;
            // }

            // var userRes = new UserModel{
            //     Id = user.Id,
            //     Email = user.Email
            // };

            
            // return userRes;
            throw new NotImplementedException();

        }

        public async Task<UserModel> RegisterAsync(SignUpModel signUpModel)
        {
            // var existingUser = await _userRepository.FindByEmailAsync(signUpModel.Email);

            // if (existingUser != null)
            // {
            //     return null;
            // }

            // var newUser = new User
            // {
            //     UserName = signUpModel.UserName,
            //     Email = signUpModel.Email,
            //     //TODO: password
            // };

            // var result = await _userRepository.AddAsync(newUser);

            // if (!result.Succeeded)
            // {
            //     // Handle errors occurred during user creation
            //     throw new Exception("Failed to register user.");
            // }

            // var userRes = new UserModel{
            //     Id = newUser.Id,
            //     Email = newUser.Email
            // };

            // return userRes;
            throw new NotImplementedException();

        }
    }
}
