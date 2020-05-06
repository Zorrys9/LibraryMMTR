using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Extensions
{
    public static class ControllerExtension
    {

        public static IActionResult Alert(this Controller controller, string message, int statusCode)
        {

            var result = new ContentResult();
            string url = "";
            if(statusCode == 400)
            {

                url = "document.referrer;";

            }
            else
            {

                url = "window.location.replace('AllBooks');";

            }

            result.Content = "<script>alert('" + message + "');" + url + "</script>";
            result.StatusCode = statusCode;
            result.ContentType = "html";

            return result;
        }

    }
}
