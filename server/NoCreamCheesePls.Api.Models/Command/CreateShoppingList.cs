using MediatR;
using NoCreamCheesePls.Api.Models.CommandResults;

namespace NoCreamCheesePls.Api.Models.Command
{
  public class CreateShoppingList : IRequest<CreateShoppingListResult>
  { }
}
