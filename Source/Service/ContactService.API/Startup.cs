using ContactService.API.Extension;
using ContactService.Application;
using ContactService.Application.Exception;
using ContactService.ContactModule.Engine;
using ContactService.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;



namespace ContactService.API
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
            IMvcBuilder mvcBuilder = services.AddControllers(options => options.Filters.Add<ApiResponseExceptionFilter>())
                                            .ConfigureApiBehaviorOptions(options =>
                                            {
                                                options.SuppressModelStateInvalidFilter = true;
                                                options.SuppressInferBindingSourcesForParameters = true;
                                            });

            services.AddControllers();

            services.AddSwaggerConfiguration();

            services.AddCoreApplication();

            services.AddCoreInfrastructure();

            //Modules
            services.AddContactModuleEngine(mvcBuilder, Configuration);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseOpenApi();

            app.UseSwaggerUi3();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
