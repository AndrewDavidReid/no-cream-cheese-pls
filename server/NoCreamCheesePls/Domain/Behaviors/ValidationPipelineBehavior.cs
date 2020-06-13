using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using NoCreamCheesePls.Domain.Exceptions;

namespace NoCreamCheesePls.Domain.Behaviors
{
  public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
  {
    public ValidationPipelineBehavior(IValidator<TRequest>[] validators)
    {
      _mValidators = validators;
    }

    private readonly IValidator<TRequest>[] _mValidators;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      var context = new ValidationContext(request);

      var error_messages = _mValidators.Select(v => v.Validate(context)).SelectMany(r => r.Errors.Select(error => error.ErrorMessage)).ToList();

      if (error_messages.Any())
      {
        throw new BadRequestException(error_messages);
      }

      return await next();
    }
  }
}
