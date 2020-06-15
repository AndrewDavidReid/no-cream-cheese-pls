using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;
using NoCreamCheesePls.Domain.Exceptions;

namespace NoCreamCheesePls.Domain.CommandHandlers
{
    public class UpdateShoppingListItemHandler : IRequestHandler<UpdateShoppingListItem>
    {
      public UpdateShoppingListItemHandler(IDataStore dataStore)
      {
        _dataStore = dataStore;
      }

      private readonly IDataStore _dataStore;

      public async Task<Unit> Handle(UpdateShoppingListItem request, CancellationToken cancellationToken)
      {
        var shoppingList = await _dataStore.ShoppingList.GetByIdAsync(request.ShoppingListId);

        if (shoppingList == null)
        {
          throw new BadRequestException($"Failed to find shopping list item with ID: {request.Id} and Shopping List ID: {request.ShoppingListId}");
        }

        var shoppingListItem = shoppingList.Items.FirstOrDefault(x => x.Id == request.Id) ?? throw new BadRequestException("Invalid ID");
        shoppingListItem.Update(request.Text, request.Completed);

        _dataStore.ShoppingList.Store(shoppingList);
        await _dataStore.CommitChangesAsync();

        return Unit.Value;
      }
    }
}
