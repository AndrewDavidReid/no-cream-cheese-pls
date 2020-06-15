using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.DataModels;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;

namespace NoCreamCheesePls.Domain.CommandHandlers
{
  public class CreateShoppingListHandler : IRequestHandler<CreateShoppingList, CreateShoppingListResult>
  {
    public CreateShoppingListHandler(IDataStore dataStore)
    {
      _dataStore = dataStore;
    }

    private readonly IDataStore _dataStore;

    public async Task<CreateShoppingListResult> Handle(CreateShoppingList request, CancellationToken cancellationToken)
    {
      var shoppingList = new ShoppingList
      {
        Id = Guid.NewGuid(),
        CreatedOn = DateTime.UtcNow
      };

      _dataStore.ShoppingList.Store(shoppingList);
      await _dataStore.CommitChangesAsync();

      return new CreateShoppingListResult
      {
        CreatedId = shoppingList.Id
      };
    }
  }
}
