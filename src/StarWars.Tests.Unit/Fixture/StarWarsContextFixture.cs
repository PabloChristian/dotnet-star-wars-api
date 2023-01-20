using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StarWars.Infrastructure.Data.Context;

namespace StarWars.Tests.Unit.Fixture
{
    public class StarWarsContextFixture
    {
        protected StarWarsContext db;

        protected static DbContextOptions<StarWarsContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<StarWarsContext>();
            builder.UseInMemoryDatabase("RealTimeDbTest")
                .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        protected static StarWarsContext GetDbInstance() => new(CreateNewContextOptions());
    }
}
