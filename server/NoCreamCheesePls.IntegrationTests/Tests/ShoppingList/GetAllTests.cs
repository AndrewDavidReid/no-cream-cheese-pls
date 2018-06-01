using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Tests.ShoppingList
{
  public class GetAllTests : IntegrationTestingBase
  {
    public GetAllTests(ApiFixture apiFixture) : base(apiFixture)
    { }

    [Fact]
    public async Task WhenCalled_WithoutAnyKnownItems_ReturnsWithoutAnyItems()
    {
      var results = await ApiClient.ShoppingList.GetAllAsync();

      results.Count().Should().Be(0);
    }

    [Fact]
    public async Task WhenCalled_WithKnownItem_ReturnsWithItem()
    {
      await AddShoppingList();

      var results = await ApiClient.ShoppingList.GetAllAsync();

      results.Count().Should().Be(1);
    }
  }
}
