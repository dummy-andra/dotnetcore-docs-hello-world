using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MyWebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            HostingEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment HostingEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Get value from Azure configuration tab using key provided
            var ENVIRONMENT_VALUE = Configuration["APPSETTING_environment_stage"];

            if (!string.IsNullOrEmpty(ENVIRONMENT_VALUE))
            {
                // Create a new configuration object that uses the app.{ENVIRONMENT_VALUE}.json file.
                var configurationBuilder = new ConfigurationBuilder()
                    .SetBasePath(HostingEnvironment.ContentRootPath)
                    .AddJsonFile($"appsettings.{ENVIRONMENT_VALUE}.json", optional: false, reloadOnChange: true)
                    .AddConfiguration(Configuration) // Add the original IConfiguration instance to preserve other settings
                    .AddEnvironmentVariables();

                // Use this new configuration object to configure your ASP.NET Core application.
                Configuration = configurationBuilder.Build();

                services.AddSingleton(Configuration);
            }

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}



// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddRazorPages();

// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();
// app.UseStaticFiles();

// app.UseRouting();

// app.UseAuthorization();

// app.MapRazorPages();

// app.Run();
