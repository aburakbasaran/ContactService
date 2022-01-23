using ContactService.Infrastructure.Provider.EfDbProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace ContactService.Infrastructure.Provider.EfDbProvider.Helpers
{
    public static class DbContextBuilderHelper
    {
        public static DbContextOptionsBuilder<T> GetDbContextBuilder<T>() where T : BaseDbContext
        {
            DirectoryInfo solutionDirectory = new DirectoryInfo(Directory.GetCurrentDirectory())?.Parent?.Parent?.Parent;
            if (solutionDirectory != null)
            {
                string appSettingsDirectory = Path.Combine(solutionDirectory.FullName, "Service/ContactService.API");
                IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(appSettingsDirectory)
                    .AddJsonFile("appsettings.json").Build();
                DbContextOptionsBuilder<T> builder = new();
                builder.UseNpgsql(configuration.GetConnectionString("PostgreSqlConnection"));
                builder.UseSnakeCaseNamingConvention();
                return builder;
            }

            throw new DirectoryNotFoundException($"{nameof(solutionDirectory)} does not exist or could not be found.");
        }

        public static T GetDbContext<T>() where T : BaseDbContext
        {
            DbContextOptionsBuilder<T> builder = GetDbContextBuilder<T>();
            return (T)Activator.CreateInstance(typeof(T), builder.Options);
        }
    }
}