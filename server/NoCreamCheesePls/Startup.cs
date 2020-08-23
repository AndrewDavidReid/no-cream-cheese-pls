using System.IO;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoCreamCheesePls.Domain.CommandHandlers;
using NoCreamCheesePls.Domain.Validators;
using NoCreamCheesePls.Infrastructure.Config;
using NoCreamCheesePls.Infrastructure.DataAccess;
using NoCreamCheesePls.Infrastructure.Filters;
using NoCreamCheesePls.Middleware;

namespace NoCreamCheesePls
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      _appConfig = new AppConfig(configuration);
    }

    private IAppConfig _appConfig;

    public void ConfigureServices(IServiceCollection services)
    {
      services
        .AddControllers(x =>
          x.Filters.Add<ApiExceptionFilter>());

      // Config
      services.AddTransient<IAppConfig, AppConfig>();
      services.AddConfiguredMarten(_appConfig.DatabaseConnectionString);
      services.AddValidatorsFromAssemblyContaining(typeof(CreateShoppingListItemValidator));
      services.AddMediatR(typeof(CreateShoppingListHandler));

      services.AddOpenApiDocument(x => x.Title = "No Cream Cheese Pls - V1");

      services.AddSpaStaticFiles(options =>
      {
        options.RootPath = "wwwroot";
      });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
      if (webHostEnvironment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      // Generate import-map.json file dynamically.
      app.Map("/assets/import-map.json", x =>
      {
        x.UseMiddleware<ImportMapMiddleware>();
      });

      app.UseOpenApi();
      app.UseSwaggerUi3();

      // When not in development mode, use SpaStaticFiles
      if (!webHostEnvironment.IsDevelopment())
      {
        app.UseSpaStaticFiles();
      }

      app.UseRouting();
      // Authentication/authorization would go here
      app.UseEndpoints(x =>
      {
        x.MapDefaultControllerRoute();
      });

      app.UseSpa(options =>
      {
        // Proxy all spa requests to localhost:4200 when developing locally.
        if (webHostEnvironment.IsDevelopment())
        {
          options.UseProxyToSpaDevelopmentServer("http://localhost:4200");
        }
      });
    }
  }
}
