using Npgsql;

namespace NoCreamCheesePls.Infrastructure.Connection
{
  public interface IDbConnectionFactory
  {
    NpgsqlConnection GetConnectionInstance();
  }
}
