using FluentValidation;
using MediatR;
using NoCreamCheesePls.Business.Behaviors;
using NoCreamCheesePls.Business.Validators;
using StructureMap;

namespace NoCreamCheesePls.Bootloader.Registries
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
