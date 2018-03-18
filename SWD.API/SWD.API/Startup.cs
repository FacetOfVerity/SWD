using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using SWD.API.Filters;
using SWD.Domain;

namespace SWD.API
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            services.AddMvc(options =>
                {
                    options.Filters.AddService(typeof(ExceptionFilter));
                })
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            services.AddScoped<ExceptionFilter>();
            services.AddScoped<SwaggerSchemeProcessor>();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Info { Version = "v1", Title = "API", Description = "Service API" });
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Service API");
                options.RoutePrefix = "help";
            });

            app.UseMvcWithDefaultRoute();
        }
    }
}
