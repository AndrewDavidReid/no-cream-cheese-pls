using System;
using System.Collections.Generic;

namespace NoCreamCheesePls.Api.Models.DataModels
{
  public class ShoppingList
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }

    public IList<ShoppingListItem> Items { get; set; } = new List<ShoppingListItem>();

    public void AddItem(ShoppingListItem shoppingListItem)
    {
      Items.Add(shoppingListItem);
    }
  }
}
