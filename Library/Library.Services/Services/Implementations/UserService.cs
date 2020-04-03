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



        /// <summary>
        /// Создание нового пользователя (регистрация)
        /// </summary>
        /// <param name="model"> Модель нового пользователя </param>
        /// <returns> Результат выполнения метода </returns>
        public async Task<IdentityResult> CreateUser(RegisterViewModel model)
        {
            if (model != null)
            {

                UserEntityModel newUser = new UserEntityModel
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    SecondName = model.SecondName,
                    Patronymic = model.Patronymic,
                    UserName = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                var result = await _userManager.CreateAsync(newUser, model.Password);

                if (result.Succeeded)
                {

                    return await _userManager.AddToRoleAsync(newUser, model.Role);

                }
                else
                {

                    return null;

                }

            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns> Результат авторизации </returns>
        public SignInResult Authorization(LogInViewModel model)
        {
            if (model != null)
            {

                return _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false).Result;

            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Возвращает модель пользователя по его Id
        /// </summary>
        /// <param name="id"> Id пользователя </param>
        /// <returns> Модель пользователя </returns>
        public UserModel GetUserById(string id)
        {
            if (id != null)
            {
                var user = _userManager.FindByIdAsync(id).Result;

                UserModel result = new UserModel()
                {
                    FirstName = user.FirstName,
                    SecondName = user.SecondName,
                    Patronymic = user.Patronymic,
                    Email = user.Email
                };

                return result;
            }
            else
            {

                return null;

            }
        }

        /// <summary>
        /// Выход из своей учетной записи
        /// </summary>
        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }

    }
}
