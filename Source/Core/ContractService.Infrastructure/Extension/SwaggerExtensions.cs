using ContactService.Infrastructure.Constant;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using System;

namespace ContactService.Infrastructure.Extension
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerDocument(configure =>
            {
                configure.PostProcess = document =>
                {
                    document.Info.Version = SwaggerConstants.InfoVersion1;
                    document.Info.Title = SwaggerConstants.InfoTitle;
                    document.Info.Description = SwaggerConstants.InfoDescription;
                    document.Info.Contact = new()
                    {
                        Name = SwaggerConstants.InfoContactName,
                        Url = SwaggerConstants.Description
                    };
                };

                configure.AddSecurity(SwaggerConstants.Bearer, Array.Empty<string>(), new()
                {
                    Description = SwaggerConstants.Description,
                    Name = SwaggerConstants.Authorization,
                    In = OpenApiSecurityApiKeyLocation.Header,
                    Type = OpenApiSecuritySchemeType.ApiKey,
                    Scheme = SwaggerConstants.Bearer,
                    BearerFormat = SwaggerConstants.Jwt,
                });
            });
            return services;
        }
    }
}
