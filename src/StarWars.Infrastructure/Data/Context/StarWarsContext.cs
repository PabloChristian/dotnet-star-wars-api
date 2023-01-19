using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StarWars.Domain.Entity;
using StarWars.Infrastructure.Data.Mapping;

namespace StarWars.Infrastructure.Data.Context
{
    public class StarWarsContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public StarWarsContext(DbContextOptions<StarWarsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
#if DEBUG
                 .AddJsonFile($"appsettings.Development.json")
#else
                 .AddJsonFile($"appsettings.Production.json")
#endif
                 .Build();

            optionsBuilder.UseSqlServer(config.GetConnectionString("StarWarsConnection"));
        }
    }
}
