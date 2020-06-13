using System.Threading.Tasks;
using FluentAssertions;
using NoCreamCheesePls.Api.Client;
using NoCreamCheesePls.Data.Repositories;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Infrastructure.Connection;
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
    private DbConnectionFactory _dbConnectionFactory => _apiFixture.Server.Services.GetService(typeof(IDbConnectionFactory)).As<DbConnectionFactory>();


    protected TestObjects TestObjects { get; } = new TestObjects();

    private IShoppingListRepository ShoppingListRepository => new ShoppingListSqlRepository(_dbConnectionFactory);

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
      await ShoppingListRepository.CreateShoppingListAsync(TestObjects.KnownShoppingList);
    }

    protected async Task AddShoppingListItem()
    {
      await ShoppingListRepository.CreateShoppingListItemAsync(TestObjects.KnownShoppingListItem);
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

      using (var conn = _dbConnectionFactory.GetConnectionInstance())
      {
        await conn.OpenAsync();

        await checkpoint.Reset(conn);
      }
    }
  }
}
