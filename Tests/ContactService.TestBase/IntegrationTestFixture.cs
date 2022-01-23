using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactService.TestBase
{
    public abstract class IntegrationTestFixture
    {
        protected IServiceProvider ServiceProvider { get; }

        protected IConfiguration Configuration { get; set; }
        protected IDictionary<string, string> InMemoryConfigurations { get; set; }

        protected IntegrationTestFixture()
        {
            IServiceCollection services = new ServiceCollection();
            InMemoryConfigurations = new Dictionary<string, string>();
            BuildConfigurations();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        protected virtual void ConfigureServices(IServiceCollection services)
        {
        }

        protected virtual void BuildConfigurations()
        {
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(InMemoryConfigurations)
                .Build();
        }

        protected T GetService<T>()
        {
            return ServiceProvider.GetService<T>();
        }

        protected T GetRequiredService<T>()
        {
            return ServiceProvider.GetRequiredService<T>();
        }

        protected IServiceScope CreateScope()
        {
            return ServiceProvider.CreateScope();
        }
    }
}
