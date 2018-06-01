using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Business.Exceptions;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.Repositories.Abstractions;

namespace NoCreamCheesePls.Business.CommandHandlers
{
  public class CreateShoppingListItemHandler : IRequestHandler<CreateShoppingListItem, CreateShoppingListItemResult>
  {
    public CreateShoppingListItemHandler(IShoppingListRepository shoppingListRepository)
    {
      m_ShoppingListRepository = shoppingListRepository;
    }

    private readonly IShoppingListRepository m_ShoppingListRepository;

    public async Task<CreateShoppingListItemResult> Handle(CreateShoppingListItem request, CancellationToken cancellationToken)
    {
      var item = await m_ShoppingListRepository.GetByIdAsync(request.ShoppingListId);

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

      var result = await m_ShoppingListRepository.CreateShoppingListItemAsync(shopping_list_item);

      return new CreateShoppingListItemResult
      {
        CreatedItemId = shopping_list_item.Id
      };
    }
  }
}
