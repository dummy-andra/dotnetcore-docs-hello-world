using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace YourWebApiNamespace
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Your other service configurations...

            // Retrieve the environment_stage value from the app settings.
            string environmentStage = _configuration["environment_stage"];

            // Use the environment_stage value to change app.settings file or perform other actions.
            // For example, you can choose different settings based on the environment_stage value.
            if (environmentStage == "SIT")
            {
                // Load different settings for SIT environment.
                // You can use ConfigurationBuilder here to load a different app.settings file or modify settings accordingly.
            }
            else if (environmentStage == "PROD")
            {
                // Load different settings for PROD environment.
                // Similarly, you can use ConfigurationBuilder here to load a different app.settings file or modify settings accordingly.
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Your middleware configurations...
        }
    }
}
