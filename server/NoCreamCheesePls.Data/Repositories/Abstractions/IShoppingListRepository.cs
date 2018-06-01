using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.ReadModels;

namespace NoCreamCheesePls.Data.Repositories.Abstractions
{
  public interface IShoppingListRepository
  {
    Task<int> CreateShoppingListAsync(ShoppingList shoppingListP);
    Task<int> CreateShoppingListItemAsync(ShoppingListItem item);
    Task<ShoppingList> GetByIdAsync(Guid id);
    Task<ShoppingListItem> GetItemByIdAndListIdAsync(Guid itemId, Guid shoppingListId);
    Task<IEnumerable<ShoppingList>> GetAllShoppingListsAsync();
    Task<ShoppingListWithItems> GetShoppingListWithItemsAsync(Guid shoppingListId);
    Task<int> UpdateShoppingListItemAsync(ShoppingListItem update);
  }
}
