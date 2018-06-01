using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.QueryResults;
using NoCreamCheesePls.Business.Queries.Abstractions;

namespace NoCreamCheesePls.Controllers.api
{
  [Route("api/shopping-list")]
  [ApiController]
  public class ShoppingListController : ControllerBase
  {
    public ShoppingListController(IMediator mediatorP,
                                  IShoppingListQueries shoppingListQueries)
    {
      m_Mediator = mediatorP;
      m_ShoppingListQueries = shoppingListQueries;
    }

    private readonly IMediator m_Mediator;
    private readonly IShoppingListQueries m_ShoppingListQueries;

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<ShoppingListQueryResult>>> GetAll()
    {
      return Ok(await m_ShoppingListQueries.GetAllShoppingListsAsync());
    }

    [HttpGet]
    [Route("{id}/with-items")]
    public async Task<ActionResult<ShoppingListWithItemsQueryResult>> GetWithItems(Guid id)
    {
      var shopping_list = await m_ShoppingListQueries.GetShoppingListWithItemsAsync(id);

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
      return await m_Mediator.Send(new CreateShoppingList());
    }

    [HttpPost]
    [Route("create-item")]
    public async Task<ActionResult<CreateShoppingListItemResult>> CreateItem(CreateShoppingListItem command)
    {
      return await m_Mediator.Send(command);
    }

    [HttpPut]
    [Route("update-item")]
    public async Task<ActionResult> UpdateItem(UpdateShoppingListItem command)
    {
      await m_Mediator.Send(command);

      return Ok();
    }
  }
}
