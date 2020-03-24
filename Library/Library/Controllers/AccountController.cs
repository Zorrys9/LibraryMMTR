using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Common.ViewModels;
using Library.Logic.Services;
using Library.Logic.Services.Implementations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public AccountController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult LogIn()
        {
            return new ViewResult();
        }
        [HttpGet("[action]")]
        public void LogOut()
        {
           _usersService.LogOut();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult LogIn([FromForm]LogInViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _usersService.Authorization(model);
                    if (result.Succeeded)
                        return new OkObjectResult(model);
                    else return new BadRequestObjectResult(model);
                }
                else return new BadRequestObjectResult("При авторизации произошла ошибка!");

            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }
        [HttpGet]
        [Route("[action]")]
        public IActionResult Registration()
        {
            return new ViewResult();
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Registration([FromForm]RegisterViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = _usersService.CreateUser(model);
                    if (result.Result.Succeeded)
                        return new OkObjectResult(model);
                    else
                        return new BadRequestObjectResult(model);
                }
                else return new BadRequestObjectResult("При регистрации пользователя возникла ошибка");
            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }

        }




    }
}