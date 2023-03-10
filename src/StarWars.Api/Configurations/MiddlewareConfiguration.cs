using StarWars.Api.Middlewares;

namespace StarWars.Api.Configurations
{
    public static class MiddlewareConfiguration
    {
        public static void AddMiddlewares(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestHandlerMiddleware>();
            app.UseMiddleware<ResponseHandlerMiddleware>();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            
        }
    }
}
