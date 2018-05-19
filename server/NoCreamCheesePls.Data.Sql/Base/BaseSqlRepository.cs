using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace NoCreamCheesePls.Data.Sql.Base
{
  public abstract class BaseSqlRepository
  {
    protected BaseSqlRepository(IConfiguration configuration)
    {
      m_Configuration = configuration;
    }

    protected readonly IConfiguration m_Configuration;

    protected IDbConnection GetConnection => new NpgsqlConnection(m_Configuration.GetConnectionString("Default") + "Enlist=true;");
  }
}
