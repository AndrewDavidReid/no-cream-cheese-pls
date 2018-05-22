using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests.ShoppingList
{
  public class CreateTests : IntegrationTestingBase
  {
    public CreateTests(ApiFixture apiFixture) : base(apiFixture)
    { }

    [Fact]
    public async Task WhenCalled_ReturnsCreatedListId()
    {
      var response = await ApiClient.ShoppingList.Create();

      response.CreatedId.Should().NotBeEmpty();
    }
  }
}
