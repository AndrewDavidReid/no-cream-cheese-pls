using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Api.Models.QueryResults;

namespace NoCreamCheesePls.Business.Queries.Abstractions
{
  public interface IShoppingListQueries
  {
    Task<IEnumerable<ShoppingListQueryResult>> GetAllShoppingListsAsync();
  }
}
