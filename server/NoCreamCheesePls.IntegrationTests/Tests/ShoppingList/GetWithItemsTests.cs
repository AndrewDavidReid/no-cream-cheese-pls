using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using NoCreamCheesePls.IntegrationTests.Extensions;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests.ShoppingList
{
  public class GetWithItemsTests : IntegrationTestingBase
  {
    public GetWithItemsTests(ApiFixture apiFixture) : base(apiFixture)
    { }

    [Fact]
    public async Task WhenCalled_WithIdForItemThatDoesNotExist_ReturnsNotFound()
    {
      await ApiClient.ShoppingList.GetWithItemsAsync(TestObjects.KnownShoppingListId).ShouldThrowWithStatusCode(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task WhenCalled_ForKnownListWithoutItems_ReturnsListWithoutItems()
    {
      await AddShoppingList();

      // If this doesn't throw, we're good.
      var shopping_list = await ApiClient.ShoppingList.GetWithItemsAsync(TestObjects.KnownShoppingListId);

      shopping_list.Should().NotBeNull();
      shopping_list.Items.Should().BeEmpty();
    }

    [Fact]
    public async Task WhenCalled_ForKnownListWithItem_ReturnsListWithItem()
    {
      await AddShoppingList();
      await AddShoppingListItem();

      // If this doesn't throw, we're good.
      var shopping_list = await ApiClient.ShoppingList.GetWithItemsAsync(TestObjects.KnownShoppingListId);

      shopping_list.Id.Should().Be(TestObjects.KnownShoppingListId);
      shopping_list.Should().NotBeNull();
      shopping_list.Items.Count().Should().Be(1);
    }
  }
}
