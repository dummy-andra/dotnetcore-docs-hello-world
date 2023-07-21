# .NET 7 Hello World

This sample demonstrates a tiny Hello World .NET Core app for [App Service Web App](https://docs.microsoft.com/azure/app-service-web). 
This sample can be used in a .NET Azure App Service app as well as in a Custom Container Azure App Service app.


### Azure Web App Environment-Based Configuration
In this example, we'll modify the application to dynamically load configuration settings based on a custom environment variable named environment_stage. This variable can be set in the Azure configuration and determines which configuration file to use.

To achieve this, we'll make the following changes:

In the Startup.cs file, modify the ConfigureServices method to load the appropriate configuration file based on the value of APPSETTING_environment_stage. If the variable is not set, it will use the default appsettings.json file.

```
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
```

2. In the Index.cshtml.cs file, modify the OnGet method to read the value of environment_stage and set the appropriate message accordingly.
```
        public void OnGet()
        {
            var environmentStage = _configuration["environment_stage"];
    
            if (!string.IsNullOrEmpty(environmentStage))
            {
                Message = _configuration["Message"];
            }
            else
            {
                Message = "Hello, World!";
            }
        }
```

3. Create appsetting.SIT.json, from this file the app will read the configurations based on the Environment set in Azure Web App Settings
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "System": "Warning"
    }
  },
  "AllowedHosts": "*",
  "environment_stage": "SIT",
  "Message": "We are using SIT💜"
}
```
4. For testing purposes I modified Index.cshtml to display the `environment_stage` value to make sure that at deploy the application will know to read from the file that belogs to the environment set in Azure Variables 

List with files created/modified since this is a hello world forked project
- Startup.cs -- created
- Program.cs -- modified
- appsettings.SIT.json -- created
- Index.cshtml.cs  -- modified
- Index.cshtml -- modified

With these modifications, the application will display the appropriate message based on the value of environment_stage in the Azure configuration. This allows you to manage different configuration settings for various environments and dynamically adapt the application's behavior based on the environment variable defined in the Azure configuration.

If Environment is set to SIT
![image](https://github.com/dummy-andra/dotnetcore-docs-hello-world/assets/37038210/1ce3f86a-fedf-452d-8487-f0ceb1af6962)

![image](https://github.com/dummy-andra/dotnetcore-docs-hello-world/assets/37038210/41a98c91-7248-4d72-9630-97e0b96205d5)

If Environment is set to DEV
![image](https://github.com/dummy-andra/dotnetcore-docs-hello-world/assets/37038210/022a225d-95c0-4e26-917b-95c971fb3de8)
![image](https://github.com/dummy-andra/dotnetcore-docs-hello-world/assets/37038210/b366c696-0fed-4295-b3be-a97c295d860b)


And if no Environment is set:
![image](https://github.com/dummy-andra/dotnetcore-docs-hello-world/assets/37038210/5e070416-2dd2-4d8b-903d-ccd871d91d59)

# Contributing

This project has adopted the [Microsoft Open Source Code of Conduct](https://opensource.microsoft.com/codeofconduct/). For more information see the [Code of Conduct FAQ](https://opensource.microsoft.com/codeofconduct/faq/) or contact [opencode@microsoft.com](mailto:opencode@microsoft.com) with any additional questions or comments.
