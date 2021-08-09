using Library.Common.ViewModels;
using Library.Exceptions;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IUserService _usersService;
        private readonly ILogger _logger;

        public AccountController(IUserService usersService, ILogger logger)
        {
            _usersService = usersService;
            _logger = logger;
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
                throw new Exception("TestError");
                return View();
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Выход из аккаунта текущего пользователя
        /// </summary>
        /// <returns> Результат выхода </returns>
        [HttpPost("[action]")]
        public IActionResult LogOut()
        {
            try
            {
                _usersService.LogOut();
                return Ok();
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        [HttpGet("[action]")]
        public IActionResult AccessDenied()
        {
            try
            {
                return View();
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns> Результат авторизации </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn([FromForm] LogInViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Данные введены не корректно");
                }

                await _usersService.Authorization(model);
                return Ok();
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
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
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model"> Модель регистрации </param>
        /// <returns> Результат регистрации </returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Registration([FromForm] RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Данные для регистрации указаны не верно");
                }

                await _usersService.CreateUser(model);

                return Ok("Вы успешно зарегистрированы");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Проверка доступна ли электронная почта для регистрации
        /// </summary>
        /// <param name="email"> Электронная почта пользователя </param>
        /// <returns> Результат проверки </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CheckEmail([FromForm] string email)
        {
            try
            {
                await _usersService.CheckEmail(email);
                return Ok();
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Проверка доступно ли имя пользователя для регистрации
        /// </summary>
        /// <param name="userName"> Имя пользователя </param>
        /// <returns> Результат проверки </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CheckUserName([FromForm] string userName)
        {
            try
            {
                await _usersService.CheckUserName(userName);

                return Ok();
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }
    }
}