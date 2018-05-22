using System;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.IntegrationTests
{
  public class TestObjects
  {
    public Guid KnownShoppingListId => new Guid("302de6ae-e28e-4a3d-bb8a-78e217b9764e");

    public ShoppingList KnownShippingList => new ShoppingList
    {
      Id = KnownShoppingListId,
      CreatedOn = DateTime.UtcNow
    };
  }
}
