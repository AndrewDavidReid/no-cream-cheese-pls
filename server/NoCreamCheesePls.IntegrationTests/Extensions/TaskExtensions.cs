using System.Net;
using Refit;
using Xunit;

namespace System.Threading.Tasks
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
