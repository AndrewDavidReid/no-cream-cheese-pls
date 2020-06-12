using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Api.Models.QueryResults;

namespace NoCreamCheesePls.Domain.Queries.Abstractions
{
  public interface IShoppingListQueries
  {
    Task<IEnumerable<ShoppingListQueryResult>> GetAllShoppingListsAsync();
    Task<ShoppingListWithItemsQueryResult> GetShoppingListWithItemsAsync(Guid id);
  }
}
