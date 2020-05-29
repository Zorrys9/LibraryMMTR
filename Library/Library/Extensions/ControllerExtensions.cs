using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Extensions
{
    public static class ControllerExtensions
    {

        public static string CurrentUser(this Controller controller)
        {

            ClaimsPrincipal currentUser = controller.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;

        }

    }
}
