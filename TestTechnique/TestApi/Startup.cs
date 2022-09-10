using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestApi.Infrastructure;

namespace TestApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment currentEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = currentEnvironment;
        }

        public IConfiguration Configuration { get; }
        
        private IWebHostEnvironment HostingEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<TestApiDbContext>(options =>
            {
                var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
                options.UseSqlServer(Configuration.GetConnectionString("Database2"), b => b.MigrationsAssembly(assemblyName))
                    .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFile("logs/sqlLogs.log")));
            });
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Title = HostingEnvironment.ApplicationName,
                    Version = "v1"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", $"{env.ApplicationName} V1");
                c.DisplayRequestDuration();
            });
            
            var scopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
            if (scopeFactory != null)
            {
                using var scope = scopeFactory.CreateScope();
                var ctx = scope.ServiceProvider.GetService<TestApiDbContext>();
                ctx.Database.MigrateAsync().GetAwaiter().GetResult();
                var contextSeeder = new TestApiContextSeeder();
                contextSeeder.SeedAsync(ctx).GetAwaiter().GetResult();
            }
        }
    }
}
