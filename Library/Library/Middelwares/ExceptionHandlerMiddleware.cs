using Library.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Serilog;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Library.Middelwares
{
    public class ExceptionHandlerMiddleware
    {

        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch(BuisnessException ex)
            {
                await HandleBuisnessExceptionMessageAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionMessageAsync(context, ex);
            }
        }


        private async Task HandleExceptionMessageAsync(HttpContext context, Exception exception)
        {
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string errorMessage = "Error : " + exception.Message;

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = errorMessage
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            _logger.Error(errorMessage, exception);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleBuisnessExceptionMessageAsync(HttpContext context, BuisnessException exception)
        {
            int statusCode = (int)HttpStatusCode.BadRequest;
            string errorMessage = "Error : " + exception.Message;

            var result = JsonConvert.SerializeObject(new
            {
                StatusCode = statusCode,
                ErrorMessage = errorMessage
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            await context.Response.WriteAsync(result);
        }
    }
}
