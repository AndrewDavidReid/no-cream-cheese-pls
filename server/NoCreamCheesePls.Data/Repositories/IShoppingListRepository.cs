using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.Data.Repositories.Interfaces
{
  public interface IShoppingListRepository
  {
    Task<IEnumerable<ShoppingList>> GetAll();
  }
}