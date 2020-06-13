using NoCreamCheesePls.Infrastructure.Config;
using Npgsql;

namespace NoCreamCheesePls.Infrastructure.Connection
{
  public class DbConnectionFactory : IDbConnectionFactory
  {
    public DbConnectionFactory(IAppConfig appConfig)
    {
      _appConfig = appConfig;
    }

    private readonly IAppConfig _appConfig;

    public NpgsqlConnection GetConnectionInstance()
    {
      return new NpgsqlConnection(_appConfig.DatabaseConnectionString);
    }
  }
}
