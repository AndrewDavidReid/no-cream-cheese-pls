using System;
using System.Collections.Generic;
using CommandLine;
using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace NoCreamCheesePls.Data.Migrations
{
  public class Program
  {
    static void Main(string[] args)
    {
      Parser.Default.ParseArguments<Options>(args)
        .WithParsed(Run)
        .WithNotParsed(OnError);

    }

    private static void Run(Options opts)
    {
      var serviceProvider = CreateServices(opts);

      // Put the database update into a scope to ensure
      // that all resources will be disposed.
      using (var scope = serviceProvider.CreateScope())
      {
        UpdateDatabase(scope.ServiceProvider);
      }
    }

    private static void OnError(IEnumerable<Error> err)
    {
      Environment.Exit(1);
    }

    /// <summary>
    /// Configure the dependency injection services
    /// </summary>
    private static IServiceProvider CreateServices(Options opts)
    {
      return new ServiceCollection()
        // Add common FluentMigrator services
        .AddFluentMigratorCore()
        .ConfigureRunner(rb => rb
          // Add SQLite support to FluentMigrator
          .AddPostgres()
          .WithGlobalConnectionString(opts.ConnectionString)
          // Define the assembly containing the migrations
          .ScanIn(typeof(Migration001AddShoppingListTable).Assembly).For.Migrations())
        // Enable logging to console in the FluentMigrator way
        .AddLogging(lb => lb.AddFluentMigratorConsole())
        // Build the service provider
        .BuildServiceProvider(false);
    }

    /// <summary>
    /// Update the database
    /// </summary>
    private static void UpdateDatabase(IServiceProvider serviceProvider)
    {
      // Instantiate the runner
      var runner = serviceProvider.GetRequiredService<IMigrationRunner>();

      // Execute the migrations
      runner.MigrateUp();
    }
  }

  internal class Options
  {
    [Option('s', HelpText = "The connection string for db connection", Required = true)]
    public string ConnectionString { get; set; }

    [Option('t', HelpText = "Tags to run")]
    public string Tags { get; set; }
  }
}
