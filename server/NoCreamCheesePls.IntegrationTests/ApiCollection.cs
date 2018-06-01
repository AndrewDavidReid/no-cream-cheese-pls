using Xunit;

namespace NoCreamCheesePls.IntegrationTests
{
  [CollectionDefinition("Api Testing Collection")]
  public class ApiCollection : ICollectionFixture<ApiFixture>
  { }
}
