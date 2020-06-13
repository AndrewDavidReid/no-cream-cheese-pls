using FluentMigrator;

namespace NoCreamCheesePls.Data.Migrations
{
  [Migration(1)]
  public class Migration001AddShoppingListTable : Migration
  {
    public override void Up()
    {
      Create.Table("shopping_list")
        .WithColumn("id").AsGuid().NotNullable().PrimaryKey()
        .WithColumn("created_on").AsDateTime().NotNullable();
    }

    public override void Down()
    {
      Delete.Table("shopping_list");
    }
  }
}
