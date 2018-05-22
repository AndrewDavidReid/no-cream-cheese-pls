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

    [HttpPost]
    [Route("create")]
    public async Task<ActionResult<CreateShoppingListResult>> Create()
    {
      return await m_Mediator.Send(new CreateShoppingList());
    }
  }
}
