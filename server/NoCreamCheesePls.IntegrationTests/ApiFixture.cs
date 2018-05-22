using Microsoft.AspNetCore.Mvc.Testing;
using NoCreamCheesePls.Api.Client;

namespace NoCreamCheesePls.IntegrationTests
{
  public class ApiFixture : WebApplicationFactory<Startup>
  {
    public ApiFixture()
    {
      ApiClient = ApiClientFactory.GetInstance(CreateClient());
    }

    public ApiClient ApiClient { get; }
  }
}
