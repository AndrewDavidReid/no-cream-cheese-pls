using System;

namespace NoCreamCheesePls.Api.Models.DataModels
{
  public class ShoppingListItem
  {
    public Guid Id { get; set; }
    public Guid ShoppingListId { get; set; }
    public string Text { get; set; }
    public bool Completed { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime LastUpdatedOn { get; set; }

    public void Update(string text, bool completed)
    {
      Text = text;
      Completed = completed;
      LastUpdatedOn = DateTime.UtcNow;
    }
  }
}
