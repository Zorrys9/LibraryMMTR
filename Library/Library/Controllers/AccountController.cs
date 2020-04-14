using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Common.ViewModels;
using Library.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _usersService;
        public AccountController(IUserService usersService)
        {
            _usersService = usersService;
        }


        /// <summary>
        /// Показывает страницу авторизации
        /// </summary>
        /// <returns> Страница реализации </returns>
        [HttpGet("[action]")]
        public IActionResult LogIn()
        {
            try
            {

                return View();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Выход из аккаунта текущего пользователя
        /// </summary>
        /// <returns> Результат выхода </returns>
        [HttpGet("[action]")]
        public IActionResult LogOut()
        {
            try
            {

                _usersService.LogOut();

                return RedirectToAction("LogIn", "Account");

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns> Результат авторизации </returns>
        [HttpPost("[action]")]
        public IActionResult LogIn([FromForm]LogInViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _usersService.Authorization(model);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("AllBooks", "Library");
                    }
                    else
                    {
                        return new BadRequestObjectResult(model);
                    }
                }
                else
                {
                    return BadRequest("При авторизации произошла ошибка!");
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Вывод страницы регистрации
        /// </summary>
        /// <returns> Страница регистрации </returns>
        [HttpGet("[action]")]
        public IActionResult Registration()
        {
            try
            {

                return View();

            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);

            }
            
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model"> Модель регистрации </param>
        /// <returns> Результат регистрации </returns>
        [HttpPost("[action]")]
        public IActionResult Registration([FromForm]RegisterViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = _usersService.CreateUser(model);

                    if (result.Result.Succeeded)
                    {
                        return RedirectToAction("AllBooks", "Library");
                    }
                    else
                    {
                        return BadRequest(model);
                    }

                }
                else
                {
                    return BadRequest("При регистрации пользователя возникла ошибка");
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




    }
}