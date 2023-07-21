using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
        // Add any required services here.
        // For example, if you want to use cookies-based authentication:
        // services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        //     .AddCookie();

        // Add the required authorization services.
        services.AddAuthorization();

        // Other service configurations and dependencies can be added here.
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // Configure the middleware pipeline here. 
        // Other middleware configurations can be added here.

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

        // Use authentication and authorization middleware.
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        // Additional middleware or endpoints can be added here.
    }
}
