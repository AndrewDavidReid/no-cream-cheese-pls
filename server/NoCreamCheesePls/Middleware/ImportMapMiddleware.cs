using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace NoCreamCheesePls.Middleware
{
  public class ImportMapMiddleware
  {
    public ImportMapMiddleware(RequestDelegate next)
    {
      _next = next;
    }

    private readonly RequestDelegate _next;

    public async Task InvokeAsync(HttpContext context)
    {
      var data = new ImportMap
      {
        Imports = new NccpImportMapDefinitions
        {
          DocsApp = "https://dev-x.y.z"
        }
      };

      var importMapText = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
      {
        ContractResolver = new DefaultContractResolver
        {
          NamingStrategy = new KebabCaseNamingStrategy()
        }
      });

      await context.Response.WriteAsync(importMapText);
    }
  }
}
