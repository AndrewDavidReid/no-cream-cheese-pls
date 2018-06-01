using System;
using Microsoft.Extensions.DependencyInjection;
using StructureMap;

namespace NoCreamCheesePls.Bootloader
{
  public class Bootstrapper
  {
    public static IServiceProvider ConfigureDependencyInjection(IServiceCollection servicesP)
    {
      return ConfigureDependencyInjection(servicesP, x => { });
    }

    public static IServiceProvider ConfigureDependencyInjection(IServiceCollection servicesP, Action<ConfigurationExpression> overridesP)
    {
      var container = new Container();

      container.Configure(config =>
      {
        config.Populate(servicesP);
        config.Scan(scanner =>
        {
          scanner.TheCallingAssembly();
          scanner.LookForRegistries();
        });

        overridesP(config);
      });

      return container.GetInstance<IServiceProvider>();
    }
  }
}
