using Library.Common.ViewModels;
using Library.Logic.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Logic.Services
{
    public interface IUsersService
    {
        Task<IdentityResult> CreateUser(RegisterViewModel model);
        SignInResult Authorization(LogInViewModel model);
        UserModel GetUserById(string id);
        void LogOut();
    }
}
