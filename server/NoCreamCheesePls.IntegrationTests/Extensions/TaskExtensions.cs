using System.Net;
using System.Threading.Tasks;
using Refit;
using Xunit;

namespace NoCreamCheesePls.IntegrationTests.Extensions
{
  public static class TaskExtensions
  {

    public static async Task ShouldThrowWithStatusCode(this Task instanceP, HttpStatusCode statusCodeP)
    {
      try
      {
        await instanceP;
      }
      catch (ApiException e)
      {
        if (e.StatusCode == statusCodeP)
        {
          return;
        }

        Assert.True(false, $"Expected ApiExcetion with StatusCode: {statusCodeP}. Actual: {e.StatusCode};");
      }

      Assert.True(false, "ApiException not thrown");
    }
  }
}
