using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NoCreamCheesePls.Bootloader;
using NoCreamCheesePls.Business.CommandHandlers;

namespace NoCreamCheesePls
{
  public class Startup
  {
    public Startup(IConfiguration configuration,
                   IHostingEnvironment hostingEnvironment)
    {
      m_Configuration = configuration;
      m_HostingEnvironment= hostingEnvironment;
    }

    private readonly IConfiguration m_Configuration;
    private readonly IHostingEnvironment m_HostingEnvironment;

    public IServiceProvider ConfigureServices(IServiceCollection services)
    {
      ConfigureDatabase();
      ConfigureMvc(services);

      // Thanks Jimmy
      services.AddAutoMapper();
      services.AddMediatR(typeof(CreateShoppingListHandler));

      return Bootstrapper.ConfigureDependencyInjection(services);
    }

    public void Configure(IApplicationBuilder app)
    {
      if (m_HostingEnvironment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }

    private void ConfigureDatabase()
    {
      Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    private void ConfigureMvc(IServiceCollection servicesP)
    {
      var mvc_configuration = servicesP.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
      mvc_configuration.AddJsonOptions(options =>
      {
        options.SerializerSettings.Formatting = Formatting.Indented;
        // Camel cases the json properties the same way as the poco
        options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
        options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        options.SerializerSettings.DateFormatHandling = DateFormatHandling.IsoDateFormat;
      });
    }
  }
}
