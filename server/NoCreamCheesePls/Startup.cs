using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app)
    {
      if (m_HostingEnvironment.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseMvc();
    }
  }
}
