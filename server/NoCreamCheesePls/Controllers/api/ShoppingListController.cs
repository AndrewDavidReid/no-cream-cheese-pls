using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NoCreamCheesePls.Api.Models.Command;
using NoCreamCheesePls.Api.Models.CommandResults;
using NoCreamCheesePls.Api.Models.DataModels;
using NoCreamCheesePls.Data.UnitOfWork.Abstractions;

namespace NoCreamCheesePls.Controllers.api
{
  [Route("api/shopping-list")]
  [ApiController]
  public class ShoppingListController : ControllerBase
  {
    public ShoppingListController(IMediator mediatorP, IDataStore dataStore)
    {
      _mediator = mediatorP;
      _dataStore = dataStore;
    }

    private readonly IMediator _mediator;
    private readonly IDataStore _dataStore;

    [HttpGet]
    [Route("all")]
    public async Task<ActionResult<IEnumerable<ShoppingList>>> GetAll()
    {
      return Ok(_dataStore.ShoppingList.GetAll());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult<ShoppingList>> GetById(Guid id)
    {
      var shoppingList = await _dataStore.ShoppingList.GetByIdAsync(id);

      if (shoppingList != null)
      {
        return shoppingList;
      }

      return NotFound();
    }

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<CreateShoppingListResult>> Create()
    {
      return await _mediator.Send(new CreateShoppingList());
    }

    [HttpPost]
    [Route("create-item")]
    public async Task<ActionResult<CreateShoppingListItemResult>> CreateItem(CreateShoppingListItem command)
    {
      return await _mediator.Send(command);
    }

    [HttpPut]
    [Route("update-item")]
    public async Task<ActionResult> UpdateItem(UpdateShoppingListItem command)
    {
      await _mediator.Send(command);

      return Ok();
    }
  }
}
