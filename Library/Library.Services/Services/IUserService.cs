using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Common.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{

    public interface IUserService
    {

        /// <summary>
        /// Создание нового пользователя (регистрация)
        /// </summary>
        /// <param name="model"> Модель нового пользователя </param>
        /// <returns> Результат выполнения метода </returns>
        Task CreateUser(RegisterViewModel model);


        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns> Результат авторизации </returns>
        Task Authorization(LogInViewModel model);

        /// <summary>
        /// Возвращает модель пользователя по его Id
        /// </summary>
        /// <param name="id"> Id пользователя </param>
        /// <returns> Модель пользователя </returns>
        Task<UserModel> GetUserById(string id);

        /// <summary>
        /// Проверка есть ли пользователь с указанной почтой
        /// </summary>
        /// <param name="email"> Электронная почта пользователя </param>
        Task CheckEmail(string email);

        /// <summary>
        /// Проверка есть ли пользователем с указаным именем
        /// </summary>
        /// <param name="userName"> Имя пользователя </param>
        Task CheckUserName(string userName);

        /// <summary>
        /// Выход из своей учетной записи
        /// </summary>
        void LogOut();
    }

    
}
