using DWShop.Shared.Wrapper;
using FluentValidation;
using System.Net;
using System.Text.Json;

namespace DWShop.Service.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<ExceptionMiddleware> logger;
        private readonly IHostEnvironment env;

        public ExceptionMiddleware(RequestDelegate next, 
            ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            this.next = next;
            this.logger = logger;
            this.env = env;
        }

        public async Task InvokeAsync(HttpContext context) 
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex) 
            {
                var errors = ex.Errors.Select(x => x.ErrorMessage).ToList();

                JsonSerializerOptions options = Serilizer(context);

                var response = Result.Fail(errors);

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);

                JsonSerializerOptions options = Serilizer(context);

                var response = env.IsDevelopment() ?
                    Result.Fail(ex.Message) : Result.Fail("Ocurrio un error interno");

                var json = JsonSerializer.Serialize(response, options);

                await context.Response.WriteAsync(json);
            }
        }

        private static JsonSerializerOptions Serilizer(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            return options;

        }

        //practica 1 punto
        /* hacer el endpoint del delete */

    }
}
