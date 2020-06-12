using System.Collections.Generic;

namespace NoCreamCheesePls.Domain.Exceptions
{
  public class ApiError
  {
    public ApiError(IEnumerable<string> errors)
    {
      Errors = errors;
    }

    public IEnumerable<string> Errors { get; }
    public string StackTrace { get; set; }
  }
}
