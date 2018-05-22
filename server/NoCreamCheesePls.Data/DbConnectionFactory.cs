using System;
using System.Data;
using Npgsql;

namespace NoCreamCheesePls.Data
{
  public static class DbConnectionFactory
  {
    public static IDbConnection GetInstance => new NpgsqlConnection(GetDefaultConnectionString + "Enlist=true");

    public static string GetDefaultConnectionString => string.Format(
      "Host={0};Port={1};Database={2};Username={3};Password={4};",
      Environment.GetEnvironmentVariable("NCCP_DB_HOST"),
      Environment.GetEnvironmentVariable("NCCP_DB_PORT"),
      Environment.GetEnvironmentVariable("NCCP_DB_NAME"),
      Environment.GetEnvironmentVariable("NCCP_DB_USER"),
      Environment.GetEnvironmentVariable("NCCP_DB_PASSWORD"));
  }
}
