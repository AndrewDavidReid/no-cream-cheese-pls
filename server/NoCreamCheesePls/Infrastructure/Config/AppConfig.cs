using Microsoft.Extensions.Configuration;

namespace NoCreamCheesePls.Infrastructure.Config
{
  public class AppConfig : IAppConfig
  {
    public AppConfig(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    private readonly IConfiguration _configuration;

    public string DatabaseConnectionString => _configuration.GetValue<string>("DatabaseConnectionString");
  }
}
