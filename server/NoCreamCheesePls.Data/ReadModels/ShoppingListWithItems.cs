using System;
using System.Collections.Generic;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.Data.ReadModels
{
  public class ShoppingListWithItems
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }

    public  IEnumerable<ShoppingListItem> Items { get;set; }
  }
}
