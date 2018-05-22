using NoCreamCheesePls.Business.Queries;
using StructureMap;

namespace NoCreamCheesePls.Bootloader.Registries
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
