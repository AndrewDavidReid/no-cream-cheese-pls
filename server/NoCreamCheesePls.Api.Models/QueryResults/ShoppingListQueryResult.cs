using System;

namespace NoCreamCheesePls.Api.Models.QueryResults
{
  public class ShoppingListQueryResult
  {
    public Guid Id { get; set; }
    public DateTime CreatedOn { get; set; }

    public int NumberOfItems { get; set; }
    public DateTime LastUpdatedOn { get; set; }
  }
}
