using System;
using MediatR;
using NoCreamCheesePls.Api.Models.CommandResults;

namespace NoCreamCheesePls.Api.Models.Command
{
  public class CreateShoppingListItem : IRequest<CreateShoppingListItemResult>
  {
    public Guid ShoppingListId { get; set; }
    public string Text { get; set; }
  }
}
