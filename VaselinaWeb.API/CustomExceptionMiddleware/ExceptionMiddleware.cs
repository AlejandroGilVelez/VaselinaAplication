using Framework.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace VaselinaWeb.API.CustomExceptionMiddleware
{
    public class ExceptionMiddleware
    {
        #region Atributos

        private readonly RequestDelegate _next;
        //private readonly ILogger _logger;

        #endregion

        #region Constructor

        public ExceptionMiddleware(RequestDelegate next)
        {
            //_logger = logger;
            _next = next;
        }

        #endregion

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                //_logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = $"Algo hicimos mal en el servidor. Mensaje de error: {exception.Message}"
            }));
        }






    }
}
