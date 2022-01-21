using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.ContactModule.Data.Data
{
    public static class ContactDbContextExtensions
    {
        public static IServiceCollection AddContactDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IContactDbContext, ContactDbContext>((dbContextOptions) =>
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