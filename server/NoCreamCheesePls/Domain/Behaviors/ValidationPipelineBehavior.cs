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
      m_Validators = validators;
    }

    private readonly IValidator<TRequest>[] m_Validators;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
      var context = new ValidationContext(request);

      var error_messages = m_Validators.Select(v => v.Validate(context)).SelectMany(r => r.Errors.Select(error => error.ErrorMessage)).ToList();

      if (error_messages.Any())
      {
        throw new BadRequestException(error_messages);
      }

      return await next();
    }
  }
}
