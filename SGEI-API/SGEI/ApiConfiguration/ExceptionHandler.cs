using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Threading.Tasks;

namespace SGEI.ApiConfiguration
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly IStringLocalizer<Program> _localizer;

        public ExceptionHandler(
            RequestDelegate next,
            IStringLocalizer<Program> localizer)
        {
            _localizer = localizer;
            _next = next;
        }

        // ReSharper disable once UnusedMember.Global
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (SqlException sqlEx)
            {
                await InternalErrorException(context, sqlEx, HttpStatusCode.ServiceUnavailable);
            }
            catch (Exception ex)
            {
                await InternalErrorException(context, ex, HttpStatusCode.InternalServerError);
            }
        }

        private Task InternalErrorException(HttpContext context, Exception ex, HttpStatusCode statusCode)
        {
            var baseException = ex.GetBaseException();
            var result = JsonConvert.SerializeObject(new
            {
                message = GetMessage(baseException.Message),
                detail = ex.GetBaseException().StackTrace
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }

        private string GetMessage(string key)
        {
            var message = _localizer.GetString(key);
            return !message.ResourceNotFound ? message.Value : $"Error message missing: {key}";
        }
    }
}
