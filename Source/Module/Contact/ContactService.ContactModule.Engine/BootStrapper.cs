using ContactService.ContactModule.Data.Data;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace ContactService.ContactModule.Engine
{
    public static class BootStrapper
    {
        public static IServiceCollection AddContactModuleEngine(this IServiceCollection services, IMvcBuilder mvcBuilder, IConfiguration configuration)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();

            // DB
            services.AddContactDbContext(configuration);

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