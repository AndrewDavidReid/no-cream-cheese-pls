using System.Threading.Tasks;
using FluentAssertions;
using Marten;
using NoCreamCheesePls.Api.Client;
using NoCreamCheesePls.Data.UnitOfWork;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;
using NoCreamCheesePls.Infrastructure.Config;
using Npgsql;
using Respawn;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests
{
  [Collection("Api Testing Collection")]
  public abstract class IntegrationTestingBase :  IAsyncLifetime
  {
    protected IntegrationTestingBase(ApiFixture apiFixture)
    {
      _apiFixture = apiFixture;
      ApiClient = apiFixture.ApiClient;
    }

    protected readonly ApiClient ApiClient;

    private readonly ApiFixture _apiFixture;
    private IDataStore _dataStore => _apiFixture.Server.Services.GetService(typeof(IDataStore)).As<MartenDataStore>();
    private IAppConfig _appConfig => _apiFixture.Server.Services.GetService(typeof(IAppConfig)).As<AppConfig>();


    protected TestObjects TestObjects { get; } = new TestObjects();


    // Called Before Each test
    public virtual async Task InitializeAsync()
    {
      await ResetData();
    }

    // Called After each test
    public virtual Task DisposeAsync()
    {
      return Task.CompletedTask;
    }

    protected async Task AddShoppingList()
    {
      _dataStore.ShoppingList.Store(TestObjects.KnownShoppingList);
      await _dataStore.CommitChangesAsync();
    }

    protected async Task AddShoppingListWithItem()
    {
      _dataStore.ShoppingList.Store(TestObjects.KnownShoppingListWithItem);
      await _dataStore.CommitChangesAsync();
    }

    private async Task ResetData()
    {
      var checkpoint = new Checkpoint
      {
        SchemasToInclude = new[]
        {
          "public"
        },
        TablesToIgnore = new[]
        {
          "VersionInfo"
        },
        DbAdapter = DbAdapter.Postgres
      };

      using (var conn = new NpgsqlConnection(_appConfig.DatabaseConnectionString))
      {
        await conn.OpenAsync();

        await checkpoint.Reset(conn);
      }
    }
  }
}
