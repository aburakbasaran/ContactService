using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.ReportModule.Data.Data
{
    public static class ContactReporDbContextExtensions
    {
        public static IServiceCollection AddContactReportDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IContactReportDbContext, ContactReportDbContext>((dbContextOptions) =>
            {
                dbContextOptions
                    .UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"),
                        opts => opts.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name))
                    .UseSnakeCaseNamingConvention();
            });

            return services;
        }
    }
}