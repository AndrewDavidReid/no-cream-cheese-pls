using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.Repositories.Abstractions;

namespace NoCreamCheesePls.Data.Repositories
{
  public class ShoppingListSqlRepository : IShoppingListRepository
  {
    public async Task<int> CreateShoppingListAsync(ShoppingList shoppingListP)
    {
      using (var connection = DbConnectionFactory.GetInstance)
      {
        var sql = @"INSERT INTO shopping_list 
                     (id, created_on)
                    VALUES
                      (@Id, @CreatedOn)";

        return await connection.ExecuteAsync(sql, shoppingListP);
      }
    }

    public async Task<IEnumerable<ShoppingList>> GetAllShoppingListsAsync()
    {
      using (var connection = DbConnectionFactory.GetInstance)
      {
        var sql = @"SELECT * 
                    FROM shopping_list";

        return await connection.QueryAsync<ShoppingList>(sql);
      }
    }
  }
}
