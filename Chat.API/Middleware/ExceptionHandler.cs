using Chat.API.Model;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Chat.API.Middleware
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Constructor for Exception handler
        /// </summary>
        /// <param name="next"></param>
        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
            //todo: Add logger instance
        }

        /// <summary>
        /// Invoice method for exception handler
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Handle exception and format the response
        /// </summary>
        /// <param name="context"></param>
        /// <param name="exception"></param>
        /// <returns></returns>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = ConfigurateExceptionTypes(exception);

            var validationError = new ErrorResponse(new ErrorModel() { Message = response.StatusCode != 500 ? exception.Message : "Internal Server Error" });
            await response.WriteAsync(JsonConvert.SerializeObject(validationError));
        }

        /// <summary>
        /// Get exception status code
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static int ConfigurateExceptionTypes(Exception exception)
        {
            var httpStatusCode = exception switch
            {
                var _ when exception.GetType().Name == typeof(ValidationException).Name => (int)HttpStatusCode.BadRequest,
                _ => (int)HttpStatusCode.InternalServerError,
            };
            return httpStatusCode;
        }
    }
}
