using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using NoCreamCheesePls.Data.Models;
using NoCreamCheesePls.Data.ReadModels;
using NoCreamCheesePls.Data.Repositories.Abstractions;
using NoCreamCheesePls.Infrastructure.Connection;

namespace NoCreamCheesePls.Data.Repositories
{
  public class ShoppingListSqlRepository : IShoppingListRepository
  {
    public ShoppingListSqlRepository(IDbConnectionFactory dbConnectionFactory)
    {
      _dbConnectionFactory = dbConnectionFactory;
    }

    private readonly IDbConnectionFactory _dbConnectionFactory;

    public async Task<int> CreateShoppingListAsync(ShoppingList shoppingListP)
    {
      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      {
        var sql = @"INSERT INTO shopping_list
                     (id, created_on)
                    VALUES
                     (@Id, @CreatedOn)";

        return await connection.ExecuteAsync(sql, shoppingListP);
      }
    }

    public async Task<int> CreateShoppingListItemAsync(ShoppingListItem item)
    {
      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      {
        var sql = @"INSERT INTO shopping_list_item
                     (id, shopping_list_id, text, completed, created_on, last_updated_on)
                    VALUES
                     (@Id, @ShoppingListId, @Text, @Completed, @CreatedOn, @LastUpdatedOn)";

        return await connection.ExecuteAsync(sql, item);
      }
    }

    public async Task<ShoppingList> GetByIdAsync(Guid id)
    {
      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      {
        var sql = "SELECT * FROM shopping_list WHERE id=@Id";

        return await connection.QueryFirstOrDefaultAsync<ShoppingList>(sql, new { Id = id});
      }
    }

    public async Task<ShoppingListItem> GetItemByIdAndListIdAsync(Guid itemId, Guid listId)
    {
      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      {
        var sql = @"SELECT *
                    FROM shopping_list_item
                    WHERE id=@Id
                      AND shopping_list_id=@ShoppingListId";

        return await connection.QueryFirstOrDefaultAsync<ShoppingListItem>(sql, new { Id = itemId, ShoppingListId = listId});
      }
    }

    public async Task<IEnumerable<ShoppingList>> GetAllShoppingListsAsync()
    {
      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      {
        var sql = @"SELECT *
                    FROM shopping_list";

        return await connection.QueryAsync<ShoppingList>(sql);
      }
    }

    public async Task<int> UpdateShoppingListItemAsync(ShoppingListItem item)
    {
      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      {
        var sql = @"UPDATE shopping_list_item
                    SET text = @Text, completed = @Completed, last_updated_on = @LastUpdatedOn
                    WHERE id=@Id AND shopping_list_id=@ShoppingListId";

        return await connection.ExecuteAsync(sql, item);
      }
    }

    public async Task<ShoppingListWithItems> GetShoppingListWithItemsAsync(Guid shoppingListId)
    {
      var sql = @"SELECT * FROM shopping_list WHERE id=@ShoppingListId;
                  SELECT * FROM shopping_list_item WHERE shopping_list_id=@ShoppingListId";

      using (var connection = _dbConnectionFactory.GetConnectionInstance())
      using (var grid = connection.QueryMultiple(sql, new { ShoppingListId = shoppingListId}))
      {
        var shoppingList = await grid.ReadFirstOrDefaultAsync<ShoppingListWithItems>();

        if (shoppingList == null)
        {
          return null;
        }

        shoppingList.Items = await grid.ReadAsync<ShoppingListItem>();

        return shoppingList;
      }
    }
  }
}
