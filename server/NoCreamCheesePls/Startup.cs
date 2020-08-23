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
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
      app.UseStaticFiles();

      app.UseOpenApi();
      app.UseSwaggerUi3();

      if (webHostEnvironment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.Use(async (context, next) =>
      {
        await next();

        if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value) &&
            !context.Request.Path.Value.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });

      app.UseRouting();
      // Authentication/authorization would go here
      app.UseEndpoints(x =>
      {
        x.MapDefaultControllerRoute();
      });
    }
  }
}
