using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NoCreamCheesePls.IntegrationTests.Extensions;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests.ShoppingList
{
  public class UpdateItemTests : IntegrationTestingBase
  {
    public UpdateItemTests(ApiFixture apiFixture) : base(apiFixture)
    { }

    [Fact]
    public async Task WhenCalled_ForShoppingListItemThatDoesNotExist_ReturnsBadRequest()
    {
      await ApiClient.ShoppingList.UpdateItemAsync(TestObjects.ValidUpdateShoppingListItem).ShouldThrowWithStatusCode(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task WhenCalled_WithValidRequest_ReturnsWithCreatedItemGuid()
    {
      await AddShoppingList();
      await AddShoppingListItem();

      // If this doesn't throw, we're good.
      await ApiClient.ShoppingList.UpdateItemAsync(TestObjects.ValidUpdateShoppingListItem);
    }
  }
}
