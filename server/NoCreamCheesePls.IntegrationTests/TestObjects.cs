using System;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.IntegrationTests
{
  public class TestObjects
  {
    public Guid KnownShoppingListId => new Guid("302de6ae-e28e-4a3d-bb8a-78e217b9764e");
    public Guid KnownShoppingListItemId => new Guid("529a0228-01b9-4771-b0ab-2f0c6ff0f24e");

    public ShoppingList KnownShoppingList => new ShoppingList
    {
      Id = KnownShoppingListId,
      CreatedOn = DateTime.UtcNow
    };

    public ShoppingListItem KnownShoppingListItem => new ShoppingListItem
    {
      Id = KnownShoppingListItemId,
      ShoppingListId = KnownShoppingListId,
      Text = "Not Cream Cheese",
      Completed = false,
      CreatedOn = DateTime.UtcNow,
      LastUpdatedOn = DateTime.UtcNow
    };

    public CreateShoppingListItem ValidCreateShoppingListItem => new CreateShoppingListItem
    {
      ShoppingListId = KnownShoppingListId,
      Text = "Not Cream Cheese"
    };

    public UpdateShoppingListItem ValidUpdateShoppingListItem => new UpdateShoppingListItem
    {
      Id = KnownShoppingListItemId,
      ShoppingListId = KnownShoppingListId,
      Text = "Definitely not Cream Cheese",
      Completed = true
    };
  }
}
