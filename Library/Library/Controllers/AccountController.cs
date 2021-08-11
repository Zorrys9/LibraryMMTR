using Library.Common.ViewModels;
using Library.Exceptions;
using Library.Services.Services;
using Microsoft.AspNetCore.Mvc;
using Serilog;
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
            return View();
        }

        /// <summary>
        /// Выход из аккаунта текущего пользователя
        /// </summary>
        /// <returns> Результат выхода </returns>
        [HttpPost("[action]")]
        public IActionResult LogOut()
        {
            _usersService.LogOut();
            return Ok();
        }

        [HttpGet("[action]")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        /// <summary>
        /// Авторизация
        /// </summary>
        /// <param name="model"> Модель авторизации </param>
        /// <returns> Результат авторизации </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> LogIn([FromForm] LogInViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Данные введены не корректно");
            }

            await _usersService.Authorization(model);
            return Ok();
        }

        /// <summary>
        /// Вывод страницы регистрации
        /// </summary>
        /// <returns> Страница регистрации </returns>
        [HttpGet("[action]")]
        public IActionResult Registration()
        {
            return View();
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="model"> Модель регистрации </param>
        /// <returns> Результат регистрации </returns>
        [HttpPut("[action]")]
        public async Task<IActionResult> Registration([FromForm] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Данные для регистрации указаны не верно");
            }
            await _usersService.CreateUser(model);

            return Ok("Вы успешно зарегистрированы");
        }

        /// <summary>
        /// Проверка доступна ли электронная почта для регистрации
        /// </summary>
        /// <param name="email"> Электронная почта пользователя </param>
        /// <returns> Результат проверки </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CheckEmail([FromForm] string email)
        {
            await _usersService.CheckEmail(email);
            return Ok();
        }

        /// <summary>
        /// Проверка доступно ли имя пользователя для регистрации
        /// </summary>
        /// <param name="userName"> Имя пользователя </param>
        /// <returns> Результат проверки </returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> CheckUserName([FromForm] string userName)
        {
            await _usersService.CheckUserName(userName);
            return Ok();
        }
    }
}