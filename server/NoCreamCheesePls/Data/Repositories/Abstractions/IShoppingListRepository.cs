using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NoCreamCheesePls.Api.Models.DataModels;

namespace NoCreamCheesePls.Data.Repositories.Abstractions
{
  public interface IShoppingListRepository
  {
    void Store(ShoppingList shoppingList);
    Task<ShoppingList> GetByIdAsync(Guid id);
    IEnumerable<ShoppingList> GetAll();
  }
}
