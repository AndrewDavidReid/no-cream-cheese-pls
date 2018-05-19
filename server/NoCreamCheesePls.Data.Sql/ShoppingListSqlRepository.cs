using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.Repositories.Interfaces;
using NoCreamCheesePls.Data.Sql.Base;

namespace NoCreamCheesePls.Data.Sql
{
  public class ShoppingListSqlRepository : BaseSqlRepository, IShoppingListRepository
  {
    public ShoppingListSqlRepository(IConfiguration configuration) : base(configuration)
    { }

    public async Task<IEnumerable<ShoppingList>> GetAll()
    {
      using (var connection = GetConnection)
      {
        var sql = @"SELECT * 
                    FROM shopping_list";

        return await connection.QueryAsync<ShoppingList>(sql);
      }
    }
  }
}
