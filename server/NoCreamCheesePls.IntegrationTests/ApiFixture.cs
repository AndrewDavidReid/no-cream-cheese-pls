using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using NoCreamCheesePls.Api.Client;

namespace NoCreamCheesePls.IntegrationTests
{
  public class ApiFixture : WebApplicationFactory<Startup>
  {
    public ApiFixture()
    {
      ApiClient = ApiClientFactory.GetInstance(CreateClient());
    }

    public ApiClient ApiClient { get; }

    protected override IHostBuilder CreateHostBuilder()
    {
      var environment_name = "Testing";

      return base.CreateHostBuilder()
        .UseEnvironment(environment_name)
        .ConfigureAppConfiguration((context, builder) => builder.AddJsonFile($"appsettings.{environment_name}.json"));
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
      // Add overrides, if needed.
      builder.ConfigureServices(services =>
      {});
    }
  }
}
