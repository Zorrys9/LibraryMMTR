using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Services.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class UserService : IUserService
    {

        private readonly SignInManager<UserEntityModel> _signInManager;
        private readonly UserManager<UserEntityModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(SignInManager<UserEntityModel> signInManager, UserManager<UserEntityModel> userManager, RoleManager<IdentityRole> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }




        public async Task<IdentityResult> CreateUser(RegisterViewModel model)
        {

                UserEntityModel newUser = new UserEntityModel
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Patronymic = model.Patronymic,
                    UserName = model.Login,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {

                    return await _userManager.AddToRoleAsync(newUser, model.Role);

                }
                else
                {

                    throw new Exception("Данные для регистрации были введены не полностью");

                }

        }

        public SignInResult Authorization(LogInViewModel model)
        {
            if (model != null)
            {

                return _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false).Result;

            }
            else
            {

                throw new Exception("Данные для авторизации введены некорректно ");

            }
        }

        public UserModel GetUserById(string id)
        {
            if (id != null)
            {
                var result = _userManager.FindByIdAsync(id).Result;

                return result;
            }
            else
            {

                throw new Exception("Id пользователя равен нулю");

            }
        }

        public async Task<bool> CheckEmail(string email)
        {
            if(email != null)
            {

                var result = await _userManager.FindByEmailAsync(email);

                if(result == null)
                {

                    return true;

                }
                else
                {

                    return false;

                }
            }
            else
            {

                return false;

            }
        }

        public async Task<bool> CheckUserName(string userName)
        {
            if(userName != null)
            {

                var result = await _userManager.FindByNameAsync(userName);

                if (result == null)
                {

                    return true;

                }
                else
                {

                    return false;

                }
            }
            else
            {

                return false;

            }
        }


        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
