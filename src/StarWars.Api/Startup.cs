using StarWars.Api.Configurations;
using StarWars.Application.AutoMapper;
using StarWars.Infrastructure.Configurations;
using StarWars.Application.Common;
using Newtonsoft.Json;
using System.Globalization;
using Newtonsoft.Json.Serialization;
using StarWars.Infrastructure.Security;
using Microsoft.EntityFrameworkCore;
using StarWars.Infrastructure.Data.Context;

namespace StarWars.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                var dateConverter = new Newtonsoft.Json.Converters.IsoDateTimeConverter
                {
                    DateTimeFormat = "yyy'-'MM'-'dd'"
                };

                options.SerializerSettings.Converters.Add(dateConverter);
                options.SerializerSettings.Culture = new CultureInfo("en-US");
                options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() };
            });

            services.AddCors(options => options.AddPolicy("CorsPolicy", builder => builder.SetIsOriginAllowed(_ => true).AllowAnyMethod().AllowAnyHeader().AllowCredentials().Build()));
            services.AddDbContext<StarWarsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("StarWarsConnection")));
            services.AddIdentitySetup(Configuration);
            AutoMapperConfig.RegisterMappings();

            services.AddSwagger();
            services.AddSingleton(AutoMapperConfig.RegisterMappings().CreateMapper());
            services.AddMvc();
            services.AddLogging();
            services.AddHttpContextAccessor();
            services.AddApplication();

            services.RegisterServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.AddMiddlewares();
            app.UseSwaggerSetup();

            app.UseRouting();

            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.AddMigration<StarWarsContext>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapSwagger();
            });
        }
    }
}
