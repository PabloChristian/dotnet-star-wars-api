using Microsoft.EntityFrameworkCore;

namespace StarWars.Api.Configurations
{
    public static class MigrationConfiguration
    {
        public static void AddMigration<T>(this IApplicationBuilder app) where T : StarWars.Infrastructure.Data.Context.StarWarsContext
        {
            using var scope = app.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetService<T>();
            dbContext?.Database.Migrate();
        }
    }
}
