using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.Application
{
    public static class BootStrapper
    {
        public static IServiceCollection AddCoreApplication(this IServiceCollection services)
        {
            // MediatR Built-In Behaviours
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPreProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPostProcessorBehavior<,>));

            return services;
        }
    }
}
