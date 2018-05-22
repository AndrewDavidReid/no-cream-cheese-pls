using AutoMapper;
using NoCreamCheesePls.Api.Models.QueryResults;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.Bootloader.Mappings
{
  public class QueryMappingProfile : Profile
  {
    public QueryMappingProfile()
    {
      CreateMap<ShoppingList, ShoppingListQueryResult>();
    }
  }
}
