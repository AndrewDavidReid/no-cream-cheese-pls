using NoCreamCheesePls.Domain.Queries;
using StructureMap;

namespace NoCreamCheesePls.Infrastructure.Registries
{
  public class BusinessRegistry : Registry
  {
    public BusinessRegistry()
    {
      Scan(x =>
      {
        x.AssemblyContainingType<ShoppingListQueries>();
        x.WithDefaultConventions();
      });
    }
  }
}
