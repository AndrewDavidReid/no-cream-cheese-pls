using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using NoCreamCheesePls.Api.Models.QueryResults;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Domain.Queries.Abstractions;

namespace NoCreamCheesePls.Domain.Queries
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

    public async Task<ShoppingListWithItemsQueryResult> GetShoppingListWithItemsAsync(Guid id)
    {
      var results = await m_ShoppingListRepository.GetShoppingListWithItemsAsync(id);

      return Mapper.Map<ShoppingListWithItemsQueryResult>(results);
    }
  }
}
