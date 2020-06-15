using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Marten;
using NoCreamCheesePls.Api.Models.DataModels;
using NoCreamCheesePls.Data.Repositories.Abstractions;

namespace NoCreamCheesePls.Data.Repositories
{
  public class ShoppingListSqlRepository : IShoppingListRepository
  {
    public ShoppingListSqlRepository(IDocumentSession documentSession)
    {
      _documentSession = documentSession;
    }

    private readonly IDocumentSession _documentSession;

    public void Store(ShoppingList shoppingList)
    {
      _documentSession.Store(shoppingList);
    }

    public async Task<ShoppingList> GetByIdAsync(Guid id)
    {
      return await _documentSession.LoadAsync<ShoppingList>(id);
    }

    public IEnumerable<ShoppingList> GetAll()
    {
      return _documentSession.Query<ShoppingList>().ToList();
    }
  }
}
