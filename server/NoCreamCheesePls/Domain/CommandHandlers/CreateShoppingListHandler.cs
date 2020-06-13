using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.Repositories.Abstractions;

namespace NoCreamCheesePls.Domain.CommandHandlers
{
  public class CreateShoppingListHandler : IRequestHandler<CreateShoppingList, CreateShoppingListResult>
  {
    public CreateShoppingListHandler(IShoppingListRepository shoppingListRepositoryP)
    {
      _ShoppingListRepository = shoppingListRepositoryP;
    }

    private readonly IShoppingListRepository _ShoppingListRepository;

    public async Task<CreateShoppingListResult> Handle(CreateShoppingList request, CancellationToken cancellationToken)
    {
      var shopping_list = new ShoppingList
      {
        Id = Guid.NewGuid(),
        CreatedOn = DateTime.UtcNow
      };

      var rows_affected = await _ShoppingListRepository.CreateShoppingListAsync(shopping_list);

      return new CreateShoppingListResult
      {
        CreatedId = shopping_list.Id
      };
    }
  }
}
