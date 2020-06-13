using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Domain.Exceptions;

namespace NoCreamCheesePls.Domain.CommandHandlers
{
  public class CreateShoppingListItemHandler : IRequestHandler<CreateShoppingListItem, CreateShoppingListItemResult>
  {
    public CreateShoppingListItemHandler(IShoppingListRepository shoppingListRepository)
    {
      _ShoppingListRepository = shoppingListRepository;
    }

    private readonly IShoppingListRepository _ShoppingListRepository;

    public async Task<CreateShoppingListItemResult> Handle(CreateShoppingListItem request, CancellationToken cancellationToken)
    {
      var item = await _ShoppingListRepository.GetByIdAsync(request.ShoppingListId);

      if (item == null)
      {
        throw new BadRequestException($"No Shopping List found with ID {request.ShoppingListId}");
      }

      var shopping_list_item = new ShoppingListItem
      {
        Id = Guid.NewGuid(),
        ShoppingListId = request.ShoppingListId,
        Completed = false,
        Text = request.Text,
        CreatedOn = DateTime.UtcNow,
        LastUpdatedOn = DateTime.UtcNow
      };

      var result = await _ShoppingListRepository.CreateShoppingListItemAsync(shopping_list_item);

      return new CreateShoppingListItemResult
      {
        CreatedItemId = shopping_list_item.Id
      };
    }
  }
}
