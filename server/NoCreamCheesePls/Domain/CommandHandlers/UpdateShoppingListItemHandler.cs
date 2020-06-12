using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Domain.Exceptions;

namespace NoCreamCheesePls.Domain.CommandHandlers
{
    public class UpdateShoppingListItemHandler : IRequestHandler<UpdateShoppingListItem>
    {
      public UpdateShoppingListItemHandler(IShoppingListRepository shoppingListRepository)
      {
        m_ShoppingListRepository = shoppingListRepository;
      }

      private readonly IShoppingListRepository m_ShoppingListRepository;

      public async Task Handle(UpdateShoppingListItem request, CancellationToken cancellationToken)
      {
        var shopping_list_item = await m_ShoppingListRepository.GetItemByIdAndListIdAsync(request.Id, request.ShoppingListId);

        if (shopping_list_item == null)
        {
          throw new BadRequestException($"Failed to find shopping list item with ID: {request.Id} and Shopping List ID: {request.ShoppingListId}");
        }

        var updated_list_item = shopping_list_item;
        updated_list_item.Text = request.Text;
        updated_list_item.LastUpdatedOn = DateTime.UtcNow;
        updated_list_item.Completed = request.Completed;

        await m_ShoppingListRepository.UpdateShoppingListItemAsync(updated_list_item);
      }
    }
}
