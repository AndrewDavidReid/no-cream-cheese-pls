using Marten;
using Marten.Services;
using Microsoft.Extensions.DependencyInjection;
using NoCreamCheesePls.Data.UnitOfWork;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;

namespace NoCreamCheesePls.Infrastructure.DataAccess
{
  public static class MartenInstaller
  {
    public static void AddConfiguredMarten(this IServiceCollection services, string cnnString)
    {
      services.AddSingleton(CreateDocumentStore(cnnString));

      services.AddScoped<IDataStore, MartenDataStore>();
    }

    private static IDocumentStore CreateDocumentStore(string cn)
    {
      return DocumentStore.For(_ =>
      {
        _.Connection(cn);
        _.Serializer(CustomizeJsonSerializer());
      });
    }

    private static JsonNetSerializer CustomizeJsonSerializer()
    {
      var serializer = new JsonNetSerializer();

      serializer.Customize(_ =>
      {
        _.ContractResolver = new ProtectedSettersContractResolver();
      });

      return serializer;
    }
  }
}
