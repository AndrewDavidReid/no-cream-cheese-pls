using System.Net.Http;
using NoCreamCheesePls.Api.Client.ApiEndpoints;
using Refit;

namespace NoCreamCheesePls.Api.Client
{
  public static class ApiClientFactory
  {
    public static ApiClient GetInstance(HttpClient clientP)
    {
      return new ApiClient
      {
        ShoppingList = RestService.For<IShoppingListEndpoint>(clientP)
      };
    }
  }
}
