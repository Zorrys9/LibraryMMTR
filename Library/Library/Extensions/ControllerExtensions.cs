using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Extensions
{
    /// <summary>
    /// Расширение контроллера
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Получение текущего пользователя системы
        /// </summary>
        /// <param name="controller">Текущий контроллер</param>
        /// <returns>Идентификатор пользователя</returns>
        public static string CurrentUser(this Controller controller)
        {
            ClaimsPrincipal currentUser = controller.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }
    }
}
