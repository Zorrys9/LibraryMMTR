using AutoMapper;
using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Data.EntityModels;
using Library.Exceptions;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Threading.Tasks;
using System.Text.Json;

namespace Library.Services.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly SignInManager<UserEntityModel> _signInManager;
        private readonly UserManager<UserEntityModel> _userManager;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UserService(SignInManager<UserEntityModel> signInManager, UserManager<UserEntityModel> userManager, IMapper mapper, ILogger logger)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task CreateUser(RegisterViewModel model)
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
                await _userManager.AddToRoleAsync(newUser, model.Role);
            }
            _logger.Information($"New user created: \n" + JsonSerializer.Serialize(model));
        }

        public async Task Authorization(LogInViewModel model)
        {
            if (model == null)
            {
                throw new BuisnessException("Данные для авторизации введены некорректно ");
            }

            var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                throw new BuisnessException("При авторизации произошла ошибка!");
            }
        }

        public async Task<UserModel> GetUserById(string id)
        {
            if (id == null)
            {
                throw new BuisnessException("Id пользователя равен нулю");
            }
            var result = await _userManager.FindByIdAsync(id);
            return _mapper.Map<UserModel>(result);
        }

        public async Task CheckEmail(string email)
        {
            var result = await _userManager.FindByEmailAsync(email);
            if(result == null)
            {
                throw new BuisnessException("Эта электронная почта уже используется в системе");
            }
        }

        public async Task CheckUserName(string userName)
        {
            var result = await _userManager.FindByNameAsync(userName);
            if(result != null)
            {
                throw new BuisnessException("Этот логин уже используется в системе");
            }
        }

        public async void LogOut()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
