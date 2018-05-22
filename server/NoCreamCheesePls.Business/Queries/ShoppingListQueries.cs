using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NoCreamCheesePls.Api.Models.QueryResults;
using NoCreamCheesePls.Business.Queries.Abstractions;
using NoCreamCheesePls.Data.Repositories.Abstractions;

namespace NoCreamCheesePls.Business.Queries
{
  public class ShoppingListQueries : IShoppingListQueries
  {
    public ShoppingListQueries(IShoppingListRepository shoppingListRepositoryP)
    {
      m_ShoppingListRepository = shoppingListRepositoryP;
    }

    private readonly IShoppingListRepository m_ShoppingListRepository;

    public async Task<IEnumerable<ShoppingListQueryResult>> GetAllShoppingListsAsync()
    {
      var results = await m_ShoppingListRepository.GetAllShoppingListsAsync();

      return Mapper.Map<IEnumerable<ShoppingListQueryResult>>(results);
    }
  }
}
