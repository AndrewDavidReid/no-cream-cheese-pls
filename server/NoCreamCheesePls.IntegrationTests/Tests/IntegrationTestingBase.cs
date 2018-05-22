using System.Threading.Tasks;
using NoCreamCheesePls.Api.Client;
using NoCreamCheesePls.Data;
using NoCreamCheesePls.Data.Repositories;
using NoCreamCheesePls.Data.Repositories.Abstractions;
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
      ApiClient = apiFixture.ApiClient;
    }

    protected ApiClient ApiClient { get; }
    protected TestObjects TestObjects { get; } = new TestObjects();

    private IShoppingListRepository ShoppingListRepository => new ShoppingListSqlRepository();

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
      await ShoppingListRepository.CreateShoppingListAsync(TestObjects.KnownShippingList);
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

      using (var conn = new NpgsqlConnection(DbConnectionFactory.GetDefaultConnectionString))
      {
        await conn.OpenAsync();

        await checkpoint.Reset(conn);
      }
    }
  }
}
