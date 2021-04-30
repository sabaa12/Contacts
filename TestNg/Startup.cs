using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestNg.Data;
using TestNg.infrastructure;

namespace TestNg
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var appSeting = services.GetAppSettings(Configuration);

            services
               .AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetDefaultConnection()))
                 .AddIdentity()
                 .AddJwtAuthentication(appSeting)
                 .AddApplicationServices()
                  .AddSwaggerGen(c =>
                  {
                      c.SwaggerDoc(
                          "v1",
                          new OpenApiInfo
                          {
                              Title = "My API",
                              Version = "v1"
                          });
                  })
                 .AddControllers() ;

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API");
                c.RoutePrefix = string.Empty;

            });

            if (env.IsDevelopment())
            {
                app.UseDatabaseErrorPage();
            }
            app.UseDeveloperExceptionPage();
            app.UseCors(x => x
              .AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());

            app.UseRouting();
           

            app.UseAuthentication();
            app.UseAuthorization();
            app.ApplyMigrations();

           

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
