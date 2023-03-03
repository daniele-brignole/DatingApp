using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Errors;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;
        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware>logger,IHostEnvironment env)
        {
            _next = next; //rappresenta il prossimo middleware 
            _logger = logger;
            _env = env;
        }
        //il framework si aspetta un metodo chiamato InvokeAsync, quindi la denominazione è obbligatoria
        public async Task InvokeAsync(HttpContext context){
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;

                var response = _env.IsDevelopment()
                    ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())// costriamo un ApiException se siamo in Development
                    : new ApiException(context.Response.StatusCode, ex.Message, "Internal Server error");

                var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase}; 
                var json = JsonSerializer.Serialize(response,options) ;  

                await  context.Response.WriteAsync(json);
            }
        }
    }
}