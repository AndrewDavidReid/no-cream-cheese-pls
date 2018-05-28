using System;
using System.Collections.Generic;

namespace NoCreamCheesePls.Api.Models.QueryResults
{
  public class ShoppingListWithItemsQueryResult
  {
    public Guid Id { get; set; }
    public IEnumerable<ShoppingListItemQueryResult> Items { get;set; }
  }
}
