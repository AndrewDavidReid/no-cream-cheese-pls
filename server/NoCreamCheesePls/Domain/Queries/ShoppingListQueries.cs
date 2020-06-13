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
    public ShoppingListQueries(IShoppingListRepository shoppingListRepositoryP, IMapper mapper)
    {
      _ShoppingListRepository = shoppingListRepositoryP;
      _mapper = mapper;
    }

    private readonly IShoppingListRepository _ShoppingListRepository;
    private readonly IMapper _mapper;

    public async Task<IEnumerable<ShoppingListQueryResult>> GetAllShoppingListsAsync()
    {
      var results = await _ShoppingListRepository.GetAllShoppingListsAsync();

      return _mapper.Map<IEnumerable<ShoppingListQueryResult>>(results);
    }

    public async Task<ShoppingListWithItemsQueryResult> GetShoppingListWithItemsAsync(Guid id)
    {
      var results = await _ShoppingListRepository.GetShoppingListWithItemsAsync(id);

      return _mapper.Map<ShoppingListWithItemsQueryResult>(results);
    }
  }
}
