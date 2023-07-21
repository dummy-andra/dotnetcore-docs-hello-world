using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

public class Program
{
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();

        // Access the configuration instance
        var configuration = host.Services.GetRequiredService<IConfiguration>();

        // Now, you can retrieve specific application settings like this:
        string environmentValue = configuration["APPSETTING_environment_stage"];
        // Use the retrieved settings as needed
        System.Console.WriteLine($"Environment value: {environmentValue}");

        host.Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}
