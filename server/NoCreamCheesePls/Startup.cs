using System.IO;
using AutoMapper;
using Dapper;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NoCreamCheesePls.Data.Repositories;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Domain.CommandHandlers;
using NoCreamCheesePls.Domain.Queries;
using NoCreamCheesePls.Domain.Queries.Abstractions;
using NoCreamCheesePls.Domain.Validators;
using NoCreamCheesePls.Infrastructure.Config;
using NoCreamCheesePls.Infrastructure.Connection;
using NoCreamCheesePls.Infrastructure.Filters;
using NoCreamCheesePls.Infrastructure.Mappings;

namespace NoCreamCheesePls
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    private readonly IConfiguration _configuration;

    public void ConfigureServices(IServiceCollection services)
    {
      ConfigureDatabase();

      services
        .AddControllers(x =>
          x.Filters.Add<ApiExceptionFilter>());

      // Config
      services.AddTransient<IAppConfig, AppConfig>();
      services.AddTransient<IDbConnectionFactory, DbConnectionFactory>();
      // Repositories
      services.AddTransient<IShoppingListRepository, ShoppingListSqlRepository>();
      // Queries
      services.AddTransient<IShoppingListQueries, ShoppingListQueries>();

      services.AddValidatorsFromAssemblyContaining(typeof(CreateShoppingListItemValidator));
      // Thanks Jimmy
      services.AddAutoMapper(typeof(QueryMappingProfile));
      services.AddMediatR(typeof(CreateShoppingListHandler));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment webHostEnvironment)
    {
      app.UseStaticFiles();

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

    private void ConfigureDatabase()
    {
      DefaultTypeMap.MatchNamesWithUnderscores = true;
    }
  }
}
