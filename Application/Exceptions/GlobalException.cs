using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ProjectAPI
{
    public class GlobalException
    {
        private readonly RequestDelegate next;
        public GlobalException(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            object errors = null;
            switch (ex)
            {
                case StatusCodeException statusCodeException:
                    errors = statusCodeException.Error;
                    context.Response.StatusCode = (int)statusCodeException.Code;
                    break;
                case Exception exception:
                    errors = string.IsNullOrWhiteSpace(exception.Message) ? "Error" : exception.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict;
                    break;
            }
            context.Response.ContentType = "application/json";
            if (errors != null)
            {
                var result = JsonConvert.SerializeObject(
                    new { errors });
                await context.Response.WriteAsync(result);
            }
        }
    }
}