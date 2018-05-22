using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.Data.Repositories.Abstractions
{
  public interface IShoppingListRepository
  {
    Task<int> CreateShoppingListAsync(ShoppingList shoppingListP);
    Task<IEnumerable<ShoppingList>> GetAllShoppingListsAsync();
  }
}
