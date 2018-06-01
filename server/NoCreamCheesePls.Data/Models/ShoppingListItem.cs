using System;

namespace NoCreamCheesePls.Data.Models
{
  public class ShoppingListItem
  {
    public Guid Id { get; set; }
    public Guid ShoppingListId { get; set; }
    public string Text { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }
  }
}
