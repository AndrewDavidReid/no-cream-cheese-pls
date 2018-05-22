using System.Threading.Tasks;
using NoCreamCheesePls.Api.Models.QueryResults;

namespace NoCreamCheesePls.Infrastructure.Queries
{
  public interface IShoppingListQueries
  {
    Task<ShoppingListQueryResult> GetAllShoppingListsAsync();
  }
}
