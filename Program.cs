using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                var envConfigFileName = $"appsettings.{env.EnvironmentName}.json";

                // Load the initial configuration from appsettings.json
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                // Load the environment-specific configuration if it exists
                config.AddJsonFile(envConfigFileName, optional: true, reloadOnChange: true);

                // Load Azure environment variables (if deployed in Azure)
                config.AddEnvironmentVariables(prefix: "APPSETTING_");
            })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}










// using Microsoft.AspNetCore.Hosting;
// using Microsoft.Extensions.Configuration;
// using Microsoft.Extensions.Hosting;

// public class Program
// {
//     public static void Main(string[] args)
//     {
//         var builder = WebApplication.CreateBuilder(args);

//         // Get value from Azure configuration tab using key provided
//         var environmentValue = builder.Configuration["APPSETTING_environment_stage"];

//         // Update the appsettings file used based on the environment_stage value (e.g., "SIT")
//         if (!string.IsNullOrEmpty(environmentValue))
//         {
//             builder.Configuration.AddJsonFile($"appsettings.{environmentValue}.json", optional: false, reloadOnChange: true);
//         }
//         // Set up detailed logging
//         builder.Logging.ClearProviders();
//         builder.Logging.AddConsole(options => options.IncludeScopes = true);
//         builder.Logging.AddDebug();
//         builder.Logging.AddEventSourceLogger();

//         // Build the web host
//         var app = builder.Build();

//         // Configure the HTTP request pipeline.
//         if (!app.Environment.IsDevelopment())
//         {
//             app.UseExceptionHandler("/Error");
//             // The default HSTS value is 30 days. You may want to change this for production scenarios.
//             app.UseHsts();
//         }

//         app.UseHttpsRedirection();
//         app.UseStaticFiles();

//         app.UseRouting();

//         app.UseAuthorization();

//         app.MapRazorPages();

//         app.Run();
//     }
// }



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
