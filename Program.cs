using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

class Program
{
    static void Main(string[] args)
    {
        var host = new HostBuilder()
            .ConfigureAppConfiguration((hostContext, config) =>
            {
                var settings = config.Build();
                var connection = settings["ConnectionStrings:AppConfig"];
                config.AddAzureAppConfiguration(options =>
                {
                    options.Connect(connection)
                           .UseFeatureFlags();
                });

                // Retrieve the "environment_stage" variable from Azure App Configuration
                var environmentStage = settings["environment_stage"];

                // Create a new configuration builder for SIT environment
                var configurationBuilder = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.SIT.json", optional: true, reloadOnChange: true);

                // Set the "environment_stage" value in the new configuration builder
                configurationBuilder.AddInMemoryCollection(new[]
                {
                    new KeyValuePair<string, string>("environment_stage", environmentStage)
                });

                // Replace the original configuration with the new one that contains "environment_stage"
                config.AddConfiguration(configurationBuilder.Build());
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Add your services here, if any
            })
            .Build();

        // Now you can access configuration values using the host's Configuration property
        var settingValue = host.Services.GetRequiredService<IConfiguration>()["SettingKey"];

        Console.WriteLine($"SettingValue: {settingValue}");

        // Run any other logic or services you need here

        host.Run();
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
