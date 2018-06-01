using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.QueryResults;
using Refit;

namespace NoCreamCheesePls.Api.Client.ApiEndpoints
{
  public interface IShoppingListEndpoint
  {
    [Post("/api/shopping-list/create")]
    Task<CreateShoppingListResult> CreateAsync();

    [Get("/api/shopping-list/all")]
    Task<IEnumerable<ShoppingListQueryResult>> GetAllAsync();

    [Get("/api/shopping-list/{id}/with-items")]
    Task<ShoppingListWithItemsQueryResult> GetWithItemsAsync(Guid id);

    [Post("/api/shopping-list/create-item")]
    Task<CreateShoppingListItemResult> CreateShoppingListItemAsync(CreateShoppingListItem command);

    [Put("/api/shopping-list/update-item")]
    Task UpdateItemAsync(UpdateShoppingListItem command);
  }
}
