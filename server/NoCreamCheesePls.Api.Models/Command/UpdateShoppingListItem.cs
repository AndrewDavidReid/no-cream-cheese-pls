using MediatR;
using System;

namespace NoCreamCheesePls.Api.Models.Command
{
  public class UpdateShoppingListItem : IRequest
  {
    public Guid Id { get; set; }
    public Guid ShoppingListId { get; set; }
    public string Text { get; set; }
    public bool Completed { get; set; }
  }
}
