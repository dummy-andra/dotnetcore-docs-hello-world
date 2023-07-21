using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Get value from Azure configuration tab using key provided
        var environmentValue = builder.Configuration["APPSETTING_environment_stage"];

        // Update the appsettings file used based on the environment_stage value (e.g., "SIT")
        if (!string.IsNullOrEmpty(environmentValue))
        {
            builder.Configuration.AddJsonFile($"appsettings.{environmentValue}.json", optional: false, reloadOnChange: true);
        }

        // Set up detailed logging
        builder.Logging.ClearProviders();
        builder.Logging.AddConsole(options => options.IncludeScopes = true);
        builder.Logging.AddDebug();
        builder.Logging.AddEventSourceLogger();

        // Build the web host
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder
                    .UseStartup<Startup>()
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.AddConsole();
                        logging.AddAzureWebAppDiagnostics();
                    });
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
