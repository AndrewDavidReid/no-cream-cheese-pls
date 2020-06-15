using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NoCreamCheesePls.IntegrationTests.Extensions;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests.ShoppingList
{
  public class GetByIdTests : IntegrationTestingBase
  {
    public GetByIdTests(ApiFixture apiFixture) : base(apiFixture)
    { }

    [Fact]
    public async Task WhenCalled_WithIdForItemThatDoesNotExist_ReturnsNotFound()
    {
      await ApiClient.ShoppingList.GetByIdAsync(TestObjects.KnownShoppingListId).ShouldThrowWithStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenCalled_ForKnownListWithoutItems_ReturnsListWithoutItems()
    {
      await AddShoppingList();

      // If this doesn't throw, we're good.
      var shoppingList = await ApiClient.ShoppingList.GetByIdAsync(TestObjects.KnownShoppingListId);

      shoppingList.Should().NotBeNull();
      shoppingList.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task WhenCalled_ForKnownListWithItem_ReturnsListWithItem()
    {
      await AddShoppingListWithItem();

      // If this doesn't throw, we're good.
      var shoppingList = await ApiClient.ShoppingList.GetByIdAsync(TestObjects.KnownShoppingListId);

      shoppingList.Id.Should().Be(TestObjects.KnownShoppingListId);
      shoppingList.Should().NotBeNull();
      shoppingList.Items.Count.Should().Be(1);
    }
  }
}
