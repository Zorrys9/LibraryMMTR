using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Logic.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.Services.Implementations
{
    public class UsersService: IUsersService
    {

        private readonly SignInManager<UsersEntityModel> _signInManager;
        private readonly UserManager<UsersEntityModel> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersService(SignInManager<UsersEntityModel> signInManager, UserManager<UsersEntityModel> userManager, RoleManager<IdentityRole> roleManager)
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
                UsersEntityModel newUser = new UsersEntityModel
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
                    return await _userManager.AddToRoleAsync(newUser, model.Role);
                else return null;

            }
            else return null;
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
            else return null;
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
                var result = _userManager.FindByIdAsync(id).Result;
                UserModel user = new UserModel()
                {
                    FirstName = result.FirstName,
                    SecondName = result.SecondName,
                    Patronymic = result.Patronymic,
                    Email = result.Email
                };

                return user;
            }
            else return null;
        }
        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
