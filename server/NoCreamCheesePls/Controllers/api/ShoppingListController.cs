using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.QueryResults;
using NoCreamCheesePls.Domain.Queries.Abstractions;

namespace NoCreamCheesePls.Controllers.api
{
  [Route("api/shopping-list")]
  [ApiController]
  public class ShoppingListController : ControllerBase
  {
    public ShoppingListController(IMediator mediatorP,
                                  IShoppingListQueries shoppingListQueries)
    {
      _mMediator = mediatorP;
      _mShoppingListQueries = shoppingListQueries;
    }

    private readonly IMediator _mMediator;
    private readonly IShoppingListQueries _mShoppingListQueries;

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<ShoppingListQueryResult>>> GetAll()
    {
      return Ok(await _mShoppingListQueries.GetAllShoppingListsAsync());
    }

    [HttpGet]
    [Route("{id}/with-items")]
    public async Task<ActionResult<ShoppingListWithItemsQueryResult>> GetWithItems(Guid id)
    {
      var shopping_list = await _mShoppingListQueries.GetShoppingListWithItemsAsync(id);

      if (shopping_list != null)
      {
        return shopping_list;
      }

      return NotFound();
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<CreateShoppingListResult>> Create()
    {
      return await _mMediator.Send(new CreateShoppingList());
    }

    [HttpPost]
    [Route("create-item")]
    public async Task<ActionResult<CreateShoppingListItemResult>> CreateItem(CreateShoppingListItem command)
    {
      return await _mMediator.Send(command);
    }

    [HttpPut]
    [Route("update-item")]
    public async Task<ActionResult> UpdateItem(UpdateShoppingListItem command)
    {
      await _mMediator.Send(command);

      return Ok();
    }
  }
}
