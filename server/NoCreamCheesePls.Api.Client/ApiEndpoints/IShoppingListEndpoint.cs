using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.QueryResults;
using Refit;

namespace NoCreamCheesePls.Api.Client.ApiEndpoints
{
  public interface IShoppingListEndpoint
  {
    [Post("/api/shopping-list/create")]
    Task<CreateShoppingListResult> Create();

    [Get("/api/shopping-list/all")]
    Task<IEnumerable<ShoppingListQueryResult>> GetAllAsync();
  }
}
