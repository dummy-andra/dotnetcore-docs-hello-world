using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public class Startup
{
    private readonly IWebHostEnvironment _hostingEnvironment;

    public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
    {
        Configuration = configuration;
        _hostingEnvironment = hostingEnvironment;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Get value from azure configuration tab using the key provided
        var environmentValue = Configuration["APPSETTING_environment_stage"];

        if (!string.IsNullOrEmpty(environmentValue))
        {
            // Create a new configuration object that uses the app.{environmentValue}.json file.
            var newConfiguration = new ConfigurationBuilder()
                .SetBasePath(_hostingEnvironment.ContentRootPath)
                .AddJsonFile($"appsettings.{environmentValue}.json", optional: false, reloadOnChange: true)
                .AddConfiguration(Configuration) // Preserve the original configuration
                .AddEnvironmentVariables()
                .Build();

            services.AddSingleton(newConfiguration);
        }
        else
        {
            // Use the original configuration if APPSETTING_environment_stage is not set.
            services.AddSingleton(Configuration);
        }

        // Add other service configurations as needed...
        // For example, you can use the Configuration object to configure other services:
        // services.Configure<MyOptions>(Configuration.GetSection("MyOptions"));

        // Register other services here...
        services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapRazorPages();
        });
        
        // Get the message from configuration
        var message = Configuration["Message"];
        app.Run(async context =>
        {
            await context.Response.WriteAsync(message);
        });
    }
}
