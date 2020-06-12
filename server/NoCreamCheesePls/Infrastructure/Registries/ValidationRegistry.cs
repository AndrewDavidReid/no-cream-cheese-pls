using FluentValidation;
using MediatR;
using NoCreamCheesePls.Domain.Behaviors;
using NoCreamCheesePls.Domain.Validators;
using StructureMap;

namespace NoCreamCheesePls.Infrastructure.Registries
{
  public class ValidationRegistry : Registry
  {
    public ValidationRegistry()
    {
      Scan(x =>
      {
        x.AssemblyContainingType<CreateShoppingListItemValidator>();
        x.WithDefaultConventions();
        x.ConnectImplementationsToTypesClosing(typeof(IValidator<>));
      });

      For(typeof(IPipelineBehavior<,>)).Use(typeof(ValidationPipelineBehavior<,>));
    }
  }
}
