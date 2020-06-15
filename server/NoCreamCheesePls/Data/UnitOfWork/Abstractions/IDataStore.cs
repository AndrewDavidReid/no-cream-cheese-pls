using System;
using System.Threading.Tasks;
using NoCreamCheesePls.Data.Repositories.Abstractions;

namespace NoCreamCheesePls.Data.UnitOfWork.Abstractions
{
  // Unit of work
  public interface IDataStore : IDisposable
  {
    IShoppingListRepository ShoppingList { get; }
    Task CommitChangesAsync();
  }
}
