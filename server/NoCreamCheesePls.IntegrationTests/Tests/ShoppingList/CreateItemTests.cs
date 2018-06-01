using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests.ShoppingList
{
  public class CreateItemTests : IntegrationTestingBase
  {
    public CreateItemTests(ApiFixture apiFixture) : base(apiFixture)
    { }

    [Fact]
    public async Task WhenCalled_ForShoppingListThatDoesNotExist_ReturnsWithoutCreatedItemGuid()
    {
      await ApiClient.ShoppingList.CreateShoppingListItemAsync(TestObjects.ValidCreateShoppingListItem).ShouldThrowWithStatusCode(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task WhenCalled_WithValidRequest_ReturnsWithCreatedItemGuid()
    {
      await AddShoppingList();

      var result = await ApiClient.ShoppingList.CreateShoppingListItemAsync(TestObjects.ValidCreateShoppingListItem);

      result.CreatedItemId.Should().NotBeEmpty();
    }
  }
}
