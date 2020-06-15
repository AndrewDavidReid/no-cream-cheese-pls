using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.DataModels;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;
using NoCreamCheesePls.Domain.Exceptions;

namespace NoCreamCheesePls.Domain.CommandHandlers
{
  public class CreateShoppingListItemHandler : IRequestHandler<CreateShoppingListItem, CreateShoppingListItemResult>
  {
    public CreateShoppingListItemHandler(IDataStore dataStore)
    {
      _dataStore = dataStore;
    }

    private readonly IDataStore _dataStore;


    public async Task<CreateShoppingListItemResult> Handle(CreateShoppingListItem request, CancellationToken cancellationToken)
    {
      var shoppingList = await _dataStore.ShoppingList.GetByIdAsync(request.ShoppingListId);

      if (shoppingList == null)
      {
        throw new BadRequestException($"No Shopping List found with ID {request.ShoppingListId}");
      }

      var shoppingListItem = new ShoppingListItem
      {
        Id = Guid.NewGuid(),
        ShoppingListId = request.ShoppingListId,
        Completed = false,
        Text = request.Text,
        CreatedOn = DateTime.UtcNow,
        LastUpdatedOn = DateTime.UtcNow
      };

      shoppingList.AddItem(shoppingListItem);
      _dataStore.ShoppingList.Store(shoppingList);

      await _dataStore.CommitChangesAsync();

      return new CreateShoppingListItemResult
      {
        CreatedItemId = shoppingListItem.Id
      };
    }
  }
}
