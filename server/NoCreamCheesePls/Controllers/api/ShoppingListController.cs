using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NoCreamCheesePls.Data.Models;

namespace NoCreamCheesePls.Controllers.api
{
  [Route("api/shopping-list")]
  [ApiController]
  public class ShoppingListController : ControllerBase
  {
    [Route("all")]
    public async Task<ActionResult<IEnumerable<ShoppingList>>> GetAll()
    {
      return NotFound();
    }
  }
}
