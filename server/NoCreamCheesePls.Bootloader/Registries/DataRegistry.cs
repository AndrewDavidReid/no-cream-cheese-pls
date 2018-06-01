using NoCreamCheesePls.Data.Repositories;
using StructureMap;

namespace NoCreamCheesePls.Bootloader.Registries
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
