using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestNg.infrastructure
{
    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnection(this IConfiguration config) =>
            config.GetConnectionString("DefaultConnection");

        public static ApplicationSetings GetAppSettings(this IServiceCollection services, IConfiguration Configuration)
        {
            var appSetingConfig = Configuration.GetSection("ApplicationSetings");
            services.Configure<ApplicationSetings>(appSetingConfig);

            var appSeting = appSetingConfig.Get<ApplicationSetings>();
            return appSeting;
        }

    }
}
