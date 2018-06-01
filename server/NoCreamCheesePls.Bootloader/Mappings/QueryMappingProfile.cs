using AutoMapper;
using NoCreamCheesePls.Api.Models.QueryResults;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.ReadModels;

namespace NoCreamCheesePls.Bootloader.Mappings
{
  public class QueryMappingProfile : Profile
  {
    public QueryMappingProfile()
    {
      CreateMap<ShoppingList, ShoppingListQueryResult>();
      CreateMap<ShoppingListItem, ShoppingListItemQueryResult>();
      CreateMap<ShoppingListWithItems, ShoppingListWithItemsQueryResult>();
    }
  }
}
