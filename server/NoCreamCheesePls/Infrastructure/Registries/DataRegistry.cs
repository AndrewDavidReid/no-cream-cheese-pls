using NoCreamCheesePls.Data.Repositories;
using StructureMap;

namespace NoCreamCheesePls.Infrastructure.Registries
{
  public class DataRegistry : Registry
  {
    public DataRegistry()
    {
      Scan(x =>
      {
        x.AssemblyContainingType<ShoppingListSqlRepository>();
        x.SingleImplementationsOfInterface();
      });
    }
  }
}
