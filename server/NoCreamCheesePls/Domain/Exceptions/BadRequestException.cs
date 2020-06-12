using System;
using System.Collections.Generic;

namespace NoCreamCheesePls.Domain.Exceptions
{
  public class BadRequestException : Exception
  {
    public BadRequestException(string message)
    {
      ErrorMessages = new List<string>{message};
    }

    public BadRequestException(IEnumerable<string> messages)
    {
      ErrorMessages = messages;
    }

    public IEnumerable<string> ErrorMessages { get; }
  }
}
