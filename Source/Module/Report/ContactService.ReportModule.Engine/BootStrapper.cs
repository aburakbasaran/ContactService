using System.Reflection;
using ContactService.ReportModule.Data.Data;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.ContactModule.Engine
{
    public static class BootStrapper
    {
        public static IServiceCollection AddReportModuleEngine(this IServiceCollection services, IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            // DB
            services.AddContactReportDbContext(configuration);

            // MediatR
            services.AddMediatR(executingAssembly);

            // Validators
            services.AddValidatorsFromAssembly(executingAssembly);

            // Add MVc Parts
            mvcBuilder.AddApplicationPart(executingAssembly);
            return services;
        }
    }
}