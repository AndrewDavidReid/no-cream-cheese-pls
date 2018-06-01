using System;

namespace NoCreamCheesePls.Api.Models.QueryResults
{
  public class ShoppingListItemQueryResult
  {
    public Guid Id { get; set; }
    public Guid ShoppingListId { get; set; }
    public string Text { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
  }
}
