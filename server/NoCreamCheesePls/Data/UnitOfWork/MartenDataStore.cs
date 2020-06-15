using System;
using System.Threading.Tasks;
using Marten;
using NoCreamCheesePls.Data.Repositories;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;

namespace NoCreamCheesePls.Data.UnitOfWork
{
  public class MartenDataStore : IDataStore
  {
    public MartenDataStore(IDocumentStore documentStore)
    {
      _documentSession = documentStore.LightweightSession();

      ShoppingList = new ShoppingListSqlRepository(_documentSession);
    }

    private readonly IDocumentSession _documentSession;

    public IShoppingListRepository ShoppingList { get; }

    public async Task CommitChangesAsync()
    {
      await _documentSession.SaveChangesAsync();
    }

    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if (disposing)
      {
        _documentSession?.Dispose();
      }
    }
  }
}
